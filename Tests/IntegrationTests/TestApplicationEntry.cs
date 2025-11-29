using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Testing.IntegrationTests;

public class TestApplicationEntry : WebApplicationFactory<Web.Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // use in memory db for testing, set in web program.cs
        builder.UseEnvironment("IntegrationTesting");
    }
}