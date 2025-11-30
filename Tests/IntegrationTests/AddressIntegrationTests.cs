using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Testing.IntegrationTests;

public class AddressIntegrationTests : IClassFixture<TestApplicationEntry>
{
    private readonly HttpClient _client;
    
    public AddressIntegrationTests(TestApplicationEntry factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAddresses_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/addresses");
        
        Assert.True(response.IsSuccessStatusCode);
    }
}