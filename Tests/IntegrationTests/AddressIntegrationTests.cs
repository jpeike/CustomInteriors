using System.Text;
using System.Text.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class AddressIntegrationTests : IClassFixture<TestApplicationEntry>
{
    private readonly HttpClient _client;

    private AddressModel AddressModel1 { get; } = new()
    {
        CustomerId = 1,
        Street = "TEST_STREET",
        City = "TEST_CITY",
        State = "TEST_STATE",
        Country = "TEST_COUNTRY",
        PostalCode = 11111,
        AddressType = "TEST_ADDRESS_TYPE",
    };
    
    private JsonSerializerOptions jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AddressIntegrationTests(TestApplicationEntry factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAllAddresses_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/addresses");

        Assert.True(response.IsSuccessStatusCode);
        Assert.NotNull(response.Content.Headers.ContentType);
        Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", response.Content.Headers.ContentType.CharSet);
    }

    [Fact]
    public async Task CreateReadDelete_Works()
    {
        // get already existing
        HttpResponseMessage originalGet = await _client.GetAsync("/api/addresses");

        Assert.True(originalGet.IsSuccessStatusCode);
        Assert.NotNull(originalGet.Content.Headers.ContentType);
        Assert.Equal("application/json", originalGet.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", originalGet.Content.Headers.ContentType.CharSet);

        string originalGetString = await originalGet.Content.ReadAsStringAsync();
        IEnumerable<AddressModel>? originalGetAddresses = JsonSerializer.Deserialize<AddressModel[]>(originalGetString, jsonOptions);

        Assert.NotNull(originalGetAddresses);

        // create new address
        HttpResponseMessage post = await _client.PostAsync("/api/addresses",
            new StringContent(JsonSerializer.Serialize(AddressModel1), new UTF8Encoding(), "application/json"));

        Assert.True(post.IsSuccessStatusCode);
        Assert.NotNull(originalGet.Content.Headers.ContentType);
        Assert.Equal("application/json", originalGet.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", originalGet.Content.Headers.ContentType.CharSet);
        
        string postString = await post.Content.ReadAsStringAsync();
        AddressModel? postAddress = JsonSerializer.Deserialize<AddressModel>(postString, jsonOptions);
        
        Assert.NotNull(postAddress);
        
        // see if returned address is the same as submitted 
        AddressModel1.WithDeepEqual(postAddress)
            .IgnoreSourceProperty(x => x.AddressId)
            .Assert();
        
        // see if you can use the customer id to get the address again
        HttpResponseMessage getId = await _client.GetAsync($"/api/addresses/{postAddress.CustomerId}");
        
        Assert.True(getId.IsSuccessStatusCode);
        Assert.NotNull(getId.Content.Headers.ContentType);
        Assert.Equal("application/json", getId.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", getId.Content.Headers.ContentType.CharSet);
        
        string getIdString = await getId.Content.ReadAsStringAsync();
        IEnumerable<AddressModel>? getIdAddresses = JsonSerializer.Deserialize<IEnumerable<AddressModel>>(getIdString, jsonOptions);
        
        Assert.NotNull(getIdAddresses);
        AddressModel? compAddress = getIdAddresses.FirstOrDefault(x => x.AddressId == postAddress.AddressId);

        postAddress.ShouldDeepEqual(compAddress);
    }
}