using Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Testing.IntegrationTests;

public class TestApplicationEntry : WebApplicationFactory<Web.Program>
{
    private AppDbContext DbContext { get; set; }
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // use in memory db for testing, set in web program.cs
        builder.UseEnvironment("IntegrationTesting");
        
        builder.ConfigureServices(services =>
        {
            // Remove existing DbContext
            ServiceDescriptor? descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            // Add in-memory DbContext
            //services.AddDbContext<AppDbContext>(options =>
                //options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
                //options.UseSqlite("Filename=:memory:"));

            // Build service provider to access DbContext
            // ServiceProvider sp = services.BuildServiceProvider();
            // using IServiceScope scope = sp.CreateScope();
            // DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // DbContext.Database.EnsureCreated();
        });
    }
}