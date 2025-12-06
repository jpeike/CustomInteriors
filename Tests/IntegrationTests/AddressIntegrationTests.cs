using System.Net.Http.Json;
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
        int numberOfAddresses = originalGetAddresses.Count();

        // create new address
        HttpResponseMessage post = await _client.PostAsJsonAsync("/api/addresses", AddressModel1);

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
        
        // try to delete address
        HttpResponseMessage delete = await _client.DeleteAsync($"/api/addresses/{postAddress.AddressId}");
        
        Assert.True(delete.IsSuccessStatusCode);
        Assert.NotNull(delete.Content.Headers.ContentType);
        Assert.Equal("application/json", delete.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", delete.Content.Headers.ContentType.CharSet);
        
        string deleteString = await delete.Content.ReadAsStringAsync();
        bool deleteSuccess = JsonSerializer.Deserialize<bool>(deleteString, jsonOptions);
        
        Assert.True(deleteSuccess);
        
        // see if delete returns false if already deleted
        HttpResponseMessage delete2 = await _client.DeleteAsync($"/api/addresses/{postAddress.AddressId}");
        
        Assert.True(delete2.IsSuccessStatusCode);
        Assert.NotNull(delete2.Content.Headers.ContentType);
        Assert.Equal("application/json", delete2.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", delete2.Content.Headers.ContentType.CharSet);
        
        string deleteString2 = await delete2.Content.ReadAsStringAsync();
        bool deleteSuccess2 = JsonSerializer.Deserialize<bool>(deleteString2, jsonOptions);
        
        Assert.False(deleteSuccess2);
        
        // get all again to see if any changes
        HttpResponseMessage endGet = await _client.GetAsync("/api/addresses");

        Assert.True(endGet.IsSuccessStatusCode);
        Assert.NotNull(endGet.Content.Headers.ContentType);
        Assert.Equal("application/json", endGet.Content.Headers.ContentType.MediaType);
        Assert.Equal("utf-8", endGet.Content.Headers.ContentType.CharSet);

        string endGetString = await endGet.Content.ReadAsStringAsync();
        IEnumerable<AddressModel>? endGetAddresses = JsonSerializer.Deserialize<AddressModel[]>(endGetString, jsonOptions);

        Assert.NotNull(endGetAddresses);
        int endNumberOfAddresses = endGetAddresses.Count();
        
        Assert.Equal(numberOfAddresses, endNumberOfAddresses);
    }
}