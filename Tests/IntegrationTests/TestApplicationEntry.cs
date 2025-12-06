using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Testing.IntegrationTests;

public class TestApplicationEntry : WebApplicationFactory<Web.Program>
{
    public AppDbContext DbContext { get; private set; }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // use in memory db for testing, set in web program.cs
        builder.UseEnvironment("IntegrationTesting");
        
        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            // Add in-memory DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("TestDb"));

            // Build service provider to access DbContext
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            DbContext.Database.EnsureCreated();
        });
    }
}