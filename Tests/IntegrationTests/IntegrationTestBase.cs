using System.Net.Http.Headers;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Testing.IntegrationTests;
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
                
                // Remove all authentication handlers added by Program.cs
                List<ServiceDescriptor> authHandlers = services
                    .Where(s => s.ServiceType == typeof(IConfigureOptions<JwtBearerOptions>))
                    .ToList();

                foreach (ServiceDescriptor handler in authHandlers)
                    services.Remove(handler);
                
                List<ServiceDescriptor> authOptions = services
                    .Where(s => s.ServiceType == typeof(IConfigureOptions<AuthenticationOptions>))
                    .ToList();

                foreach (ServiceDescriptor opt in authOptions)
                    services.Remove(opt);
                
                List<ServiceDescriptor> authZ = services
                    .Where(s => s.ServiceType == typeof(IAuthorizationHandler)
                                || s.ServiceType == typeof(IAuthorizationPolicyProvider)
                                || s.ServiceType == typeof(IConfigureOptions<AuthorizationOptions>))
                    .ToList();

                foreach (ServiceDescriptor svc in authZ)
                    services.Remove(svc);
                
                // add our own authentication
                services.AddAuthentication("IntegrationTestScheme")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("IntegrationTestScheme", options => { });
                services.AddAuthorization(options =>
                {
                    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                });
            });
        });

        // Create a scope and ensure DB schema
        IServiceScope scope = _factory.Services.CreateScope();
        DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        await DbContext.Database.EnsureCreatedAsync();

        // HttpClient for testing
        Client = _factory.CreateClient();
        Client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("IntegrationTestScheme");
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