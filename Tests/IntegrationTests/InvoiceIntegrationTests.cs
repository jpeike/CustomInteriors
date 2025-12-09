using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class InvoiceIntegrationTests : IntegrationTestBase
{
    private Invoice Invoice1 { get; } = new()
    {
        DateIssued = DateTime.Today,
        Method = "METHOD",
        SellerDetails = "SELLER_DETAILS",
    };
    
    private InvoiceModel InvoiceModel1 { get; } = new()
    {
        DateIssued = DateTime.Today,
        Method = "METHOD",
        SellerDetails = "SELLER_DETAILS",
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/invoices");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<InvoiceModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<InvoiceModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        InvoiceModel setModel = (await SeedAsync(Invoice1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/invoices");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<InvoiceModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<InvoiceModel>>();

        Assert.NotNull(getAllModels);
        InvoiceModel? compInvoice = getAllModels.FirstOrDefault(x => x.InvoiceId == setModel.InvoiceId);
        setModel.ShouldDeepEqual(compInvoice);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/invoices", InvoiceModel1);

        AssertHelpers.IsValidResponse(response);

        InvoiceModel? postModel = await response.Content.ReadFromJsonAsync<InvoiceModel>();

        Assert.NotNull(postModel);
        InvoiceModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.InvoiceId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        InvoiceModel setModel = (await SeedAsync(Invoice1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/invoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response);

        InvoiceModel? getIdModel = await response.Content.ReadFromJsonAsync<InvoiceModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        InvoiceModel setModel = (await SeedAsync(Invoice1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/invoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response);

        InvoiceModel? putModel = await response.Content.ReadFromJsonAsync<InvoiceModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        InvoiceModel setModel = (await SeedAsync(Invoice1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/invoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/invoices/{setModel.InvoiceId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}