using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class InvoiceItemIntegrationTests : IntegrationTestBase
{
    private Invoice Invoice1 { get; } = new()
    {
        DateIssued = DateTime.Today,
        Method = "METHOD",
        SellerDetails = "SELLER_DETAILS",
    };
    
    private InvoiceItem InvoiceItem1 { get; } = new()
    {
        Description = "DESCRIPTION",
        Amount = 999,
        Price = 999.99m,
    };
    
    private InvoiceItemModel InvoiceItemModel1 { get; } = new()
    {
        Description = "DESCRIPTION",
        Amount = 999,
        Price = 999.99m,
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/invoiceItems");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<InvoiceItemModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<InvoiceItemModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        InvoiceItem1.InvoiceId = invoice.InvoiceId;
        InvoiceItemModel setModel = (await SeedAsync(InvoiceItem1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/invoiceItems");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<InvoiceItemModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<InvoiceItemModel>>();

        Assert.NotNull(getAllModels);
        InvoiceItemModel? compInvoiceItem = getAllModels.FirstOrDefault(x => x.ItemId == setModel.ItemId);
        setModel.ShouldDeepEqual(compInvoiceItem);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        InvoiceItemModel1.InvoiceId = invoice.InvoiceId;
        
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/invoiceItems", InvoiceItemModel1);

        AssertHelpers.IsValidResponse(response);

        InvoiceItemModel? postModel = await response.Content.ReadFromJsonAsync<InvoiceItemModel>();

        Assert.NotNull(postModel);
        InvoiceItemModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.ItemId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        InvoiceItem1.InvoiceId = invoice.InvoiceId;
        InvoiceItemModel setModel = (await SeedAsync(InvoiceItem1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/invoiceItems/{setModel.ItemId}");

        AssertHelpers.IsValidResponse(response);

        InvoiceItemModel? getIdModel = await response.Content.ReadFromJsonAsync<InvoiceItemModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        InvoiceItem1.InvoiceId = invoice.InvoiceId;
        InvoiceItemModel setModel = (await SeedAsync(InvoiceItem1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/invoiceItems/{setModel.ItemId}");

        AssertHelpers.IsValidResponse(response);

        InvoiceItemModel? putModel = await response.Content.ReadFromJsonAsync<InvoiceItemModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        InvoiceItem1.InvoiceId = invoice.InvoiceId;
        InvoiceItemModel setModel = (await SeedAsync(InvoiceItem1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/invoiceItems/{setModel.ItemId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/invoiceItems/{setModel.ItemId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}