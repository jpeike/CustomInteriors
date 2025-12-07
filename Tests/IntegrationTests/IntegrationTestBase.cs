using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Web;

public abstract class IntegrationTestBase : IAsyncLifetime
{
    protected WebApplicationFactory<Program> Factory;
    protected HttpClient Client { get; private set; }
    protected AppDbContext DbContext { get; private set; }
    private SqliteConnection _connection;

    protected IntegrationTestBase()
    {
        //Factory = new WebApplicationFactory<Program>();
        
        Factory = new WebApplicationFactory<Program>()
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
        Factory = Factory.WithWebHostBuilder(builder =>
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
        IServiceScope scope = Factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await DbContext.Database.EnsureCreatedAsync();

        // HttpClient for testing
        Client = Factory.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await DbContext.DisposeAsync();
        await _connection.CloseAsync();
        _connection.Dispose();
        Client.Dispose();
        Factory.Dispose();
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