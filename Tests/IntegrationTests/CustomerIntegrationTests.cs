using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class CustomerIntegrationTests : IntegrationTestBase
{
    private Customer Customer1 { get; } = new()
    {
        FirstName = "FIRSTNAME",
        LastName = "LASTNAME",
        CompanyName = "COMPANY_NAME",
        PrefferedContactMethod = "PREFERRED_CONTACT_METHOD",
        CustomerType = "CUSTOMER_TYPE",
        Status = "STATUS",
        CustomerNotes = "CUSTOMER_NOTES",
    };
    
    private CustomerModel CustomerModel1 { get; } = new()
    {
        FirstName = "FIRSTNAME",
        LastName = "LASTNAME",
        CompanyName = "COMPANY_NAME",
        PrefferedContactMethod = "PREFERRED_CONTACT_METHOD",
        CustomerType = "CUSTOMER_TYPE",
        Status = "STATUS",
        CustomerNotes = "CUSTOMER_NOTES",
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/customers");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<CustomerModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<CustomerModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        CustomerModel setModel = (await SeedAsync(Customer1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/customers");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<CustomerModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<CustomerModel>>();

        Assert.NotNull(getAllModels);
        CustomerModel? compCustomer = getAllModels.FirstOrDefault(x => x.CustomerId == setModel.CustomerId);
        setModel.ShouldDeepEqual(compCustomer);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        CustomerModel1.CustomerId = customer.CustomerId;
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/customers", CustomerModel1);

        AssertHelpers.IsValidResponse(response);

        CustomerModel? postModel = await response.Content.ReadFromJsonAsync<CustomerModel>();

        Assert.NotNull(postModel);
        CustomerModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.CustomerId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        CustomerModel setModel = (await SeedAsync(Customer1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/customers/{setModel.CustomerId}");

        AssertHelpers.IsValidResponse(response);

        CustomerModel? getIdModel = await response.Content.ReadFromJsonAsync<CustomerModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        CustomerModel setModel = (await SeedAsync(Customer1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/customers/{setModel.CustomerId}");

        AssertHelpers.IsValidResponse(response);

        CustomerModel? putModel = await response.Content.ReadFromJsonAsync<CustomerModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        CustomerModel setModel = (await SeedAsync(Customer1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/customers/{setModel.CustomerId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/customers/{setModel.CustomerId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}