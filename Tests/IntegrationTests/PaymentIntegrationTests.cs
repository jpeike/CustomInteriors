using System.Net.Http.Json;
using Core;
using DeepEqual.Syntax;

namespace Testing.IntegrationTests;

public class PaymentIntegrationTests : IntegrationTestBase
{
    private Invoice Invoice1 { get; } = new()
    {
        DateIssued = DateTime.Today,
        Method = "METHOD",
        SellerDetails = "SELLER_DETAILS",
    };
    
    private Payment Payment1 { get; } = new()
    {
        PaymentDate = DateTime.Today,
        AmountPaid = 999.99m,
        Method = "METHOD",
    };
    
    private PaymentModel PaymentModel1 { get; } = new()
    {
        PaymentDate = DateTime.Today,
        AmountPaid = 999.99m,
        Method = "METHOD",
    };

    [Fact]
    public async Task GetAll_ReturnsEmptyWhenEmpty()
    {
        HttpResponseMessage response = await Client.GetAsync("/api/payments");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<PaymentModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<PaymentModel>>();

        Assert.NotNull(getAllModels);
        Assert.Empty(getAllModels);
    }

    [Fact]
    public async Task GetAll_ReturnsModels()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        Payment1.InvoiceId = invoice.InvoiceId;
        PaymentModel setModel = (await SeedAsync(Payment1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync("/api/payments");

        AssertHelpers.IsValidResponse(response);

        IEnumerable<PaymentModel>? getAllModels = await response.Content.ReadFromJsonAsync<IEnumerable<PaymentModel>>();

        Assert.NotNull(getAllModels);
        PaymentModel? compPayment = getAllModels.FirstOrDefault(x => x.PaymentId == setModel.PaymentId);
        setModel.ShouldDeepEqual(compPayment);
    }

    [Fact]
    public async Task Create_ReturnsModel()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        PaymentModel1.InvoiceId = invoice.InvoiceId;
        
        HttpResponseMessage response = await Client.PostAsJsonAsync("/api/payments", PaymentModel1);

        AssertHelpers.IsValidResponse(response);

        PaymentModel? postModel = await response.Content.ReadFromJsonAsync<PaymentModel>();

        Assert.NotNull(postModel);
        PaymentModel1.WithDeepEqual(postModel)
            .IgnoreSourceProperty(x => x.PaymentId)
            .Assert();
    }

    [Fact]
    public async Task GetById_ReturnsModel()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        Payment1.InvoiceId = invoice.InvoiceId;
        PaymentModel setModel = (await SeedAsync(Payment1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/payments/{setModel.PaymentId}");

        AssertHelpers.IsValidResponse(response);

        PaymentModel? getIdModel = await response.Content.ReadFromJsonAsync<PaymentModel>();

        Assert.NotNull(getIdModel);
        setModel.ShouldDeepEqual(getIdModel);
    }

    [Fact]
    public async Task Update_ReturnsModel()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        Payment1.InvoiceId = invoice.InvoiceId;
        PaymentModel setModel = (await SeedAsync(Payment1)).ToModel();

        HttpResponseMessage response = await Client.GetAsync($"/api/payments/{setModel.PaymentId}");

        AssertHelpers.IsValidResponse(response);

        PaymentModel? putModel = await response.Content.ReadFromJsonAsync<PaymentModel>();

        Assert.NotNull(putModel);
        setModel.ShouldDeepEqual(putModel);
    }

    [Fact]
    public async Task Delete_ReturnsTrueThenFalse()
    {
        Invoice invoice = (await SeedAsync(Invoice1));
        Payment1.InvoiceId = invoice.InvoiceId;
        PaymentModel setModel = (await SeedAsync(Payment1)).ToModel();

        HttpResponseMessage response = await Client.DeleteAsync($"/api/payments/{setModel.PaymentId}");

        AssertHelpers.IsValidResponse(response);

        bool deleteModel = await response.Content.ReadFromJsonAsync<bool>();

        Assert.True(deleteModel);

        // try to delete again, should return false
        HttpResponseMessage response2 = await Client.DeleteAsync($"/api/payments/{setModel.PaymentId}");

        AssertHelpers.IsValidResponse(response2);

        bool deleteModel2 = await response2.Content.ReadFromJsonAsync<bool>();

        Assert.False(deleteModel2);
    }
}