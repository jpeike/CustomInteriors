using System.Net.Http.Json;
using System.Text.Json;
using Core;
using DeepEqual.Syntax;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Testing.IntegrationTests;

public class AddressIntegrationTests : IClassFixture<TestApplicationEntry>
{
    private readonly TestApplicationEntry _factory;
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
    
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AddressIntegrationTests(TestApplicationEntry factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    /// <summary>
    /// directly insert a model into the db
    /// </summary>
    /// <returns> model return from db </returns>
    private async Task<AddressModel> SeedDb()
    {
        // set db to be scoped to just this test
        using IServiceScope scope = _factory.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        AddressModel setModel = (await db.AddAsync(AddressModel1.ToEntity())).Entity.ToModel();
        await db.SaveChangesAsync();
        return setModel;
    }

    [Fact]
    public async Task GetAllAddresses_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await _client.GetAsync("/api/addresses");

        AssertHelpers.IsValidResponse(response);
        
        string getAllString = await response.Content.ReadAsStringAsync();
        IEnumerable<AddressModel>? getAllModels = JsonSerializer.Deserialize<IEnumerable<AddressModel>>(getAllString, _jsonOptions);
        
        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }
    
    [Fact]
    public async Task GetAllAddresses_ReturnsModels()
    {
        using IServiceScope scope = _factory.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        AddressModel setModel = (await db.AddAsync(AddressModel1.ToEntity())).Entity.ToModel();
        await db.SaveChangesAsync();
        
        HttpResponseMessage response = await _client.GetAsync("/api/addresses");

        AssertHelpers.IsValidResponse(response);
        
        string getAllString = await response.Content.ReadAsStringAsync();
        IEnumerable<AddressModel>? getAllModels = JsonSerializer.Deserialize<IEnumerable<AddressModel>>(getAllString, _jsonOptions);
        
        Assert.NotNull(getAllModels);
        AddressModel? compAddress = getAllModels.FirstOrDefault(x => x.AddressId == setModel.AddressId);
        setModel.ShouldDeepEqual(compAddress);
    }
    
    [Fact]
    public async Task Create_ReturnsModel()
    {
        HttpResponseMessage response = await _client.PostAsJsonAsync("/api/addresses", AddressModel1);

        AssertHelpers.IsValidResponse(response);
        
        string postString = await response.Content.ReadAsStringAsync();
        AddressModel? postModel = JsonSerializer.Deserialize<AddressModel>(postString, _jsonOptions);
        
        Assert.NotNull(postModel);
        AddressModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.AddressId)
            .Assert();
    }
    
    [Fact]
    public async Task GetById_ReturnsModel()
    {
        using IServiceScope scope = _factory.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        AddressModel setModel = (await db.AddAsync(AddressModel1.ToEntity())).Entity.ToModel();
        await db.SaveChangesAsync();
        
        // see if you can use the customer id to get the address again
        HttpResponseMessage getId = await _client.GetAsync($"/api/addresses/{setModel.CustomerId}");
        
        AssertHelpers.IsValidResponse(getId);
        
        string getIdString = await getId.Content.ReadAsStringAsync();
        IEnumerable<AddressModel>? getIdModel = JsonSerializer.Deserialize<IEnumerable<AddressModel>>(getIdString, _jsonOptions);
        
        Assert.NotNull(getIdModel);
        AddressModel? compAddress = getIdModel.FirstOrDefault(x => x.AddressId == setModel.AddressId);

        setModel.ShouldDeepEqual(compAddress);
    }
    
    [Fact]
    public async Task Update_ReturnsModel()
    {
        using IServiceScope scope = _factory.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        AddressModel setModel = (await db.AddAsync(AddressModel1.ToEntity())).Entity.ToModel();
        await db.SaveChangesAsync();
        
        // see if you can use the customer id to get the address again
        HttpResponseMessage getId = await _client.GetAsync($"/api/addresses/{setModel.CustomerId}");
        
        AssertHelpers.IsValidResponse(getId);
        
        string getIdString = await getId.Content.ReadAsStringAsync();
        IEnumerable<AddressModel>? getIdAddresses = JsonSerializer.Deserialize<IEnumerable<AddressModel>>(getIdString, _jsonOptions);
        
        Assert.NotNull(getIdAddresses);
        AddressModel? compAddress = getIdAddresses.FirstOrDefault(x => x.AddressId == setModel.AddressId);

        setModel.ShouldDeepEqual(compAddress);
    }
    
    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        using IServiceScope scope = _factory.Services.CreateScope();
        AppDbContext db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        AddressModel setModel = (await db.AddAsync(AddressModel1.ToEntity())).Entity.ToModel();
        await db.SaveChangesAsync();
        
        // see if you can use the customer id to get the address again
        HttpResponseMessage delete = await _client.DeleteAsync($"/api/addresses/{setModel.AddressId}");
        
        AssertHelpers.IsValidResponse(delete);
        
        string deleteString = await delete.Content.ReadAsStringAsync();
        bool deleteBool = JsonSerializer.Deserialize<bool>(deleteString, _jsonOptions);
        
        Assert.True(deleteBool);
        
        // try to delete again, should return false
        HttpResponseMessage delete2 = await _client.DeleteAsync($"/api/addresses/{setModel.AddressId}");
        
        AssertHelpers.IsValidResponse(delete2);
        
        string delete2String = await delete.Content.ReadAsStringAsync();
        bool delete2Bool = JsonSerializer.Deserialize<bool>(delete2String, _jsonOptions);
        
        Assert.True(delete2Bool);
    }
}