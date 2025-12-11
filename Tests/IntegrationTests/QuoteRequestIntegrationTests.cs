using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class QuoteRequestIntegrationTests : IntegrationTestBase
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

    private QuoteRequest QuoteRequest1 { get; } = new()
    {
        JobId = 1,
        DescriptionOfWork =  "DESCRIPTION_WORK",
        EstimatedPrice = 999.99m,
        RequestDate =  DateTime.Today,
    };
    
    private QuoteRequestModel QuoteRequestModel1 { get; } = new()
    {
        JobId = 1,
        DescriptionOfWork =  "DESCRIPTION_WORK",
        EstimatedPrice = 999.99m,
        RequestDate =  DateTime.Today,
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/quoteRequests");

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
        QuoteRequest1.JobId = job.JobId;
        QuoteRequestModel setModel = (await SeedAsync(QuoteRequest1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/quoteRequests");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<QuoteRequestModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<QuoteRequestModel>>();

        Assert.NotNull(getAllModels);
        QuoteRequestModel? compQR = getAllModels.FirstOrDefault(x => x.QuoteId == setModel.QuoteId);
        setModel.ShouldDeepEqual(compQR);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        QuoteRequestModel1.JobId = job.JobId;
        
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/quoteRequests", QuoteRequest1);

        AssertHelpers.IsValidResponse(response);

        QuoteRequestModel? postModel = await response.Content.ReadFromJsonAsync<QuoteRequestModel>();

        Assert.NotNull(postModel);
        QuoteRequestModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.QuoteId)
            // ignore date since it is auto-assigned in backend and will differ
            .IgnoreSourceProperty(x => x.RequestDate)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        QuoteRequest1.JobId = job.JobId;
        QuoteRequestModel setModel = (await SeedAsync(QuoteRequest1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/quoteRequests/{setModel.QuoteId}");

        AssertHelpers.IsValidResponse(response);

        QuoteRequestModel? getIdModel = await response.Content.ReadFromJsonAsync<QuoteRequestModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        QuoteRequest1.JobId = job.JobId;
        QuoteRequestModel setModel = (await SeedAsync(QuoteRequest1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/quoteRequests/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response);

        QuoteRequestModel? putModel = await response.Content.ReadFromJsonAsync<QuoteRequestModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId = customer.CustomerId;
        JobModel job = (await SeedAsync(Job1)).ToModel();
        QuoteRequest1.JobId = job.JobId;
        QuoteRequestModel setModel = (await SeedAsync(QuoteRequest1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/quoteRequests/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/quoteRequests/{setModel.JobId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}