using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class JobInvoiceIntegrationTests : IntegrationTestBase
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
    
    private Invoice Invoice1 { get; } = new()
    {
        DateIssued = DateTime.Today,
        Method = "METHOD",
        SellerDetails = "SELLER_DETAILS",
    };
    
    private JobInvoice JobInvoice1 { get; } = new()
    {
        CreatedDate = DateTime.Today,
        PercentageOfInvoice = 99.99f,
    };
    
    private JobInvoiceModel JobInvoiceModel1 { get; } = new()
    {
        CreatedDate = DateTime.Today,
        PercentageOfInvoice = 99.99f,
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/jobInvoices");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<JobInvoiceModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<JobInvoiceModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId =  customer.CustomerId;
        Job job = await SeedAsync(Job1);
        Invoice invoice = await SeedAsync(Invoice1);
        JobInvoice1.JobId = job.JobId;
        JobInvoice1.InvoiceId = invoice.InvoiceId;
        JobInvoiceModel setModel = (await SeedAsync(JobInvoice1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/jobInvoices");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<JobInvoiceModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<JobInvoiceModel>>();

        Assert.NotNull(getAllModels);
        JobInvoiceModel? compJobInvoice = getAllModels.FirstOrDefault(x => x.JobId == setModel.JobId && x.InvoiceId == invoice.InvoiceId);
        setModel.ShouldDeepEqual(compJobInvoice);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId =  customer.CustomerId;
        Job job = await SeedAsync(Job1);
        Invoice invoice = await SeedAsync(Invoice1);
        JobInvoice1.JobId = job.JobId;
        JobInvoice1.InvoiceId = invoice.InvoiceId;
        
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/jobInvoices", JobInvoiceModel1);

        AssertHelpers.IsValidResponse(response);

        JobInvoiceModel? postModel = await response.Content.ReadFromJsonAsync<JobInvoiceModel>();

        Assert.NotNull(postModel);
        JobInvoiceModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.JobId)
            .IgnoreSourceProperty(x => x.InvoiceId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId =  customer.CustomerId;
        Job job = await SeedAsync(Job1);
        Invoice invoice = await SeedAsync(Invoice1);
        JobInvoice1.JobId = job.JobId;
        JobInvoice1.InvoiceId = invoice.InvoiceId;
        JobInvoiceModel setModel = (await SeedAsync(JobInvoice1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/jobInvoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response);

        JobInvoiceModel? getIdModel = await response.Content.ReadFromJsonAsync<JobInvoiceModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId =  customer.CustomerId;
        Job job = await SeedAsync(Job1);
        Invoice invoice = await SeedAsync(Invoice1);
        JobInvoice1.JobId = job.JobId;
        JobInvoice1.InvoiceId = invoice.InvoiceId;
        JobInvoiceModel setModel = (await SeedAsync(JobInvoice1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/jobInvoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response);

        JobInvoiceModel? putModel = await response.Content.ReadFromJsonAsync<JobInvoiceModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Customer customer = (await SeedAsync(Customer1));
        Job1.CustomerId =  customer.CustomerId;
        Job job = await SeedAsync(Job1);
        Invoice invoice = await SeedAsync(Invoice1);
        JobInvoice1.JobId = job.JobId;
        JobInvoice1.InvoiceId = invoice.InvoiceId;
        JobInvoiceModel setModel = (await SeedAsync(JobInvoice1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/jobInvoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/jobInvoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}