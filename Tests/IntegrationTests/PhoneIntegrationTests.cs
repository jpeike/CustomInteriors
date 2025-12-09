using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class PhoneIntegrationTests : IntegrationTestBase
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
    
    private Phone Phone1 { get; } = new()
    {
        CustomerId = 1,
        PhoneNumber = "555-555-5555",
        PhoneType = "PHONE_TYPE",
    };
    
    private PhoneModel PhoneModel1 { get; } = new()
    {
        CustomerId = 1,
        PhoneNumber = "555-555-5555",
        PhoneType = "PHONE_TYPE",
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/phones");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<PhoneModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<PhoneModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Customer customer = (await SeedAsync(Customer1));
        PhoneModel1.CustomerId = customer.CustomerId;
        PhoneModel setModel = (await SeedAsync(Phone1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/phones");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<PhoneModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<PhoneModel>>();

        Assert.NotNull(getAllModels);
        PhoneModel? compPhone = getAllModels.FirstOrDefault(x => x.PhoneId == setModel.PhoneId);
        setModel.ShouldDeepEqual(compPhone);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        PhoneModel1.CustomerId = customer.CustomerId;
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/phones", PhoneModel1);

        AssertHelpers.IsValidResponse(response);

        PhoneModel? postModel = await response.Content.ReadFromJsonAsync<PhoneModel>();

        Assert.NotNull(postModel);
        PhoneModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.PhoneId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        PhoneModel1.CustomerId = customer.CustomerId;
        PhoneModel setModel = (await SeedAsync(Phone1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/phones/{setModel.PhoneId}");

        AssertHelpers.IsValidResponse(response);

        PhoneModel? getIdModel = await response.Content.ReadFromJsonAsync<PhoneModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        PhoneModel1.CustomerId = customer.CustomerId;
        PhoneModel setModel = (await SeedAsync(Phone1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/phones/{setModel.PhoneId}");

        AssertHelpers.IsValidResponse(response);

        PhoneModel? putModel = await response.Content.ReadFromJsonAsync<PhoneModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        PhoneModel1.CustomerId = customer.CustomerId;
        PhoneModel setModel = (await SeedAsync(Phone1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/phones/{setModel.PhoneId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/phones/{setModel.PhoneId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}