using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class UserIntegrationTests : IntegrationTestBase
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
    
    private User User1 { get; } = new()
    {
        CustomerId = 1,
        Username = "USERNAME",
        Email =  "EMAIL@ADDRESS.COM",
        CreatedOn =  DateTime.Today,
        PasswordHash = "PASSWORD_HASH",
    };
    
    private UserModel UserModel1 { get; } = new()
    {
        CustomerId = 1,
        Username = "USERNAME",
        Email =  "EMAIL@ADDRESS.COM",
        CreatedOn =  DateTime.Today,
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/users");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<UserModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<UserModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Customer customer = (await SeedAsync(Customer1));
        UserModel1.CustomerId = customer.CustomerId;
        UserModel setModel = (await SeedAsync(User1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/users");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<UserModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<UserModel>>();

        Assert.NotNull(getAllModels);
        UserModel? compUser = getAllModels.FirstOrDefault(x => x.Id == setModel.Id);
        setModel.ShouldDeepEqual(compUser);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        UserModel1.CustomerId = customer.CustomerId;
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/users", UserModel1);

        AssertHelpers.IsValidResponse(response);

        UserModel? postModel = await response.Content.ReadFromJsonAsync<UserModel>();

        Assert.NotNull(postModel);
        UserModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.Id)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        UserModel1.CustomerId = customer.CustomerId;
        UserModel setModel = (await SeedAsync(User1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/users/{setModel.Id}");

        AssertHelpers.IsValidResponse(response);

        UserModel? getIdModel = await response.Content.ReadFromJsonAsync<UserModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        UserModel1.CustomerId = customer.CustomerId;
        UserModel setModel = (await SeedAsync(User1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/users/{setModel.Id}");

        AssertHelpers.IsValidResponse(response);

        UserModel? putModel = await response.Content.ReadFromJsonAsync<UserModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        UserModel1.CustomerId = customer.CustomerId;
        UserModel setModel = (await SeedAsync(User1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/users/{setModel.Id}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/users/{setModel.Id}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}