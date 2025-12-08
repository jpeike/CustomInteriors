using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class JobAssignmentIntegrationTests : IntegrationTestBase
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

    private JobAssignment JobAssignment1 { get; } = new()
    {
        JobId = 1,
        RoleOnJob = "ROLE_ON_JOB",
        AssignmentDate = DateTime.Today,
    };
    
    private JobAssignmentModel JobAssignmentModel1 { get; } = new()
    {
        JobId = 1,
        RoleOnJob = "ROLE_ON_JOB",
        AssignmentDate = DateTime.Today,
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/jobAssignments");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<JobModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<JobModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        JobAssignment1.JobId = job.JobId;
        JobAssignmentModel setModel = (await SeedAsync(JobAssignment1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/jobAssignments");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<JobAssignmentModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<JobAssignmentModel>>();

        Assert.NotNull(getAllModels);
        JobAssignmentModel? compModel = getAllModels.FirstOrDefault(x => x.JobAssignmentId == setModel.JobAssignmentId);
        setModel.ShouldDeepEqual(compModel);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        JobAssignmentModel1.JobId = job.JobId;
        
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/jobAssignments", JobAssignment1);

        AssertHelpers.IsValidResponse(response);

        JobAssignmentModel? postModel = await response.Content.ReadFromJsonAsync<JobAssignmentModel>();

        Assert.NotNull(postModel);
        JobAssignmentModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.JobAssignmentId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        JobAssignment1.JobId = job.JobId;
        JobAssignmentModel setModel = (await SeedAsync(JobAssignment1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/jobAssignments/{setModel.JobAssignmentId}");

        AssertHelpers.IsValidResponse(response);

        JobAssignmentModel? getIdModel = await response.Content.ReadFromJsonAsync<JobAssignmentModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        JobAssignment1.JobId = job.JobId;
        JobAssignmentModel setModel = (await SeedAsync(JobAssignment1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/jobAssignments/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response);

        JobAssignmentModel? putModel = await response.Content.ReadFromJsonAsync<JobAssignmentModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        JobAssignment1.JobId = job.JobId;
        JobAssignmentModel setModel = (await SeedAsync(JobAssignment1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/jobAssignments/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/jobAssignments/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}