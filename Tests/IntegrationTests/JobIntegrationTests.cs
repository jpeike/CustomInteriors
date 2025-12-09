using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class JobIntegrationTests : IntegrationTestBase
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
    
    private Job Job1 { get; } = new()
    {
        CustomerId = 1,
        JobDescription = "JOB_DESCRIPTION",
        StartDate = DateTime.Today,
        EndDate = DateTime.Today,
        Status =  "STATUS",
    };
    
    private JobModel JobModel1 { get; } = new()
    {
        CustomerId = 1,
        JobDescription = "JOB_DESCRIPTION",
        StartDate = DateTime.Today,
        EndDate = DateTime.Today,
        Status =  "STATUS",
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/jobs");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<JobModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<JobModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Customer customer = (await SeedAsync(Customer1));
        JobModel1.CustomerId = customer.CustomerId;
        JobModel setModel = (await SeedAsync(Job1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/jobs");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<JobModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<JobModel>>();

        Assert.NotNull(getAllModels);
        JobModel? compJob = getAllModels.FirstOrDefault(x => x.JobId == setModel.JobId);
        setModel.ShouldDeepEqual(compJob);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        JobModel1.CustomerId = customer.CustomerId;
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/jobs", JobModel1);

        AssertHelpers.IsValidResponse(response);

        JobModel? postModel = await response.Content.ReadFromJsonAsync<JobModel>();

        Assert.NotNull(postModel);
        JobModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.JobId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        JobModel1.CustomerId = customer.CustomerId;
        JobModel setModel = (await SeedAsync(Job1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/jobs/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response);

        JobModel? getIdModel = await response.Content.ReadFromJsonAsync<JobModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        JobModel1.CustomerId = customer.CustomerId;
        JobModel setModel = (await SeedAsync(Job1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/jobs/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response);

        JobModel? putModel = await response.Content.ReadFromJsonAsync<JobModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        JobModel1.CustomerId = customer.CustomerId;
        JobModel setModel = (await SeedAsync(Job1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/jobs/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/jobs/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}