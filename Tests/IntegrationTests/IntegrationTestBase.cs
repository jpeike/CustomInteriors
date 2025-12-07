using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    private WebApplicationFactory<Program> _factory;
    protected HttpClient Client { get; private set; }
    private AppDbContext DbContext { get; set; }
    private SqliteConnection _connection;

    protected IntegrationTestBase()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("IntegrationTesting");
            });
    }

    public async Task InitializeAsync()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        await _connection.OpenAsync();

        // Override DbContext in the DI container
        _factory = _factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                // Remove existing DbContext
                ServiceDescriptor? descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null) services.Remove(descriptor);

                // Add SQLite in-memory DbContext
                services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlite(_connection));
            });
        });

        // Create a scope and ensure DB schema
        IServiceScope scope = _factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await DbContext.Database.EnsureCreatedAsync();

        // HttpClient for testing
        Client = _factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _connection.CloseAsync();
        await _connection.DisposeAsync();
        Client.Dispose();
        await _factory.DisposeAsync();
    }

    /// <summary>
    /// Helper to seed data
    /// </summary>
    protected async Task<T> SeedAsync<T>(T entity) where T : class
    {
        T ret = (await DbContext.AddAsync(entity)).Entity;
        await DbContext.SaveChangesAsync();
        return ret;
    }
}