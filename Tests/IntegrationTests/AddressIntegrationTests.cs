using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class AddressIntegrationTests : IntegrationTestBase
{
    private Customer Customer1 { get; } = new()
    {
        CustomerId = 1,
        FirstName = "FIRST_NAME",
        LastName = "LAST_NAME",
        CompanyName = "COMPANY_NAME",
        PrefferedContactMethod = "PREFERRED_CONTACT_METHOD",
        CustomerType = "CUSTOMER_TYPE",
        Status = "STATUS",
        CustomerNotes = "CUSTOMER_NOTES",
    };

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

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/addresses");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<AddressModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<AddressModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Customer customer = (await SeedAsync(Customer1));
        AddressModel1.CustomerId = customer.CustomerId;
        AddressModel setModel = (await SeedAsync(AddressModel1.ToEntity())).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/addresses");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<AddressModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<AddressModel>>();

        Assert.NotNull(getAllModels);
        AddressModel? compAddress = getAllModels.FirstOrDefault(x => x.AddressId == setModel.AddressId);
        setModel.ShouldDeepEqual(compAddress);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        AddressModel1.CustomerId = customer.CustomerId;
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/addresses", AddressModel1);

        AssertHelpers.IsValidResponse(response);

        AddressModel? postModel = await response.Content.ReadFromJsonAsync<AddressModel>();

        Assert.NotNull(postModel);
        AddressModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.AddressId)
            .Assert();
    }

    /// <summary>
    /// address get by id searches by customer id
    /// </summary>
    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        AddressModel1.CustomerId = customer.CustomerId;
        AddressModel setModel = (await SeedAsync(AddressModel1.ToEntity())).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/addresses/{setModel.CustomerId}");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<AddressModel>? getIdModel = await response.Content.ReadFromJsonAsync<IEnumerable<AddressModel>>();

        Assert.NotNull(getIdModel);
        AddressModel? compAddress = getIdModel.FirstOrDefault(x => x.AddressId == setModel.AddressId);

        setModel.ShouldDeepEqual(compAddress);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        AddressModel1.CustomerId = customer.CustomerId;
        AddressModel setModel = (await SeedAsync(AddressModel1.ToEntity())).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/addresses/{setModel.CustomerId}");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<AddressModel>? putModel = await response.Content.ReadFromJsonAsync<IEnumerable<AddressModel>>();

        Assert.NotNull(putModel);
        AddressModel? compAddress = putModel.FirstOrDefault(x => x.AddressId == setModel.AddressId);

        setModel.ShouldDeepEqual(compAddress);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        AddressModel1.CustomerId = customer.CustomerId;
        AddressModel setModel = (await SeedAsync(AddressModel1.ToEntity())).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/addresses/{setModel.AddressId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/addresses/{setModel.AddressId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}