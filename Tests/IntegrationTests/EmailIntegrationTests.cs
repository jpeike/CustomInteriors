using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class EmailIntegrationTests : IntegrationTestBase
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
    
    private Email Email1 { get; } = new()
    {
        CustomerId = 1,
        EmailAddress = "EMAIL@ADDRESS.COM",
        EmailType = "EMAIL_TYPE",
        CreatedOn =  DateTime.Today,
    };
    
    private EmailModel EmailModel1 { get; } = new()
    {
        CustomerId = 1,
        EmailAddress = "EMAIL@ADDRESS.COM",
        EmailType = "EMAIL_TYPE",
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/emails");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<EmailModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<EmailModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Customer customer = (await SeedAsync(Customer1));
        EmailModel1.CustomerId = customer.CustomerId;
        EmailModel setModel = (await SeedAsync(Email1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/emails");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<EmailModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<EmailModel>>();

        Assert.NotNull(getAllModels);
        EmailModel? compEmail = getAllModels.FirstOrDefault(x => x.EmailId == setModel.EmailId);
        setModel.ShouldDeepEqual(compEmail);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        EmailModel1.CustomerId = customer.CustomerId;
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/emails", EmailModel1);

        AssertHelpers.IsValidResponse(response);

        EmailModel? postModel = await response.Content.ReadFromJsonAsync<EmailModel>();

        Assert.NotNull(postModel);
        EmailModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.EmailId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        EmailModel1.CustomerId = customer.CustomerId;
        EmailModel setModel = (await SeedAsync(Email1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/emails/{setModel.EmailId}");

        AssertHelpers.IsValidResponse(response);

        EmailModel? getIdModel = await response.Content.ReadFromJsonAsync<EmailModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        EmailModel1.CustomerId = customer.CustomerId;
        EmailModel setModel = (await SeedAsync(Email1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/emails/{setModel.EmailId}");

        AssertHelpers.IsValidResponse(response);

        EmailModel? putModel = await response.Content.ReadFromJsonAsync<EmailModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        EmailModel1.CustomerId = customer.CustomerId;
        EmailModel setModel = (await SeedAsync(Email1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/emails/{setModel.EmailId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/emails/{setModel.EmailId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}