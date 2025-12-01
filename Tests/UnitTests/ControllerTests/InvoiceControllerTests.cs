using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class InvoiceControllerTests
{
    private InvoiceModel Invoice { get; } = new()
    {
        InvoiceId = 1,
        DateIssued = DateTime.Now,
        Method = "Method",
        SellerDetails = "SellerDetails"
    };

    private readonly Mock<IInvoiceService> _service;
    private readonly InvoiceController _controller;

    public InvoiceControllerTests()
    {
        _service = new Mock<IInvoiceService>();
        _controller = new InvoiceController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic invoice
        _service.Setup(s => s.GetAllInvoices())
            .ReturnsAsync(new List<InvoiceModel>());

        var result = await _controller.GetAllInvoices();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetInvoiceById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetInvoiceById(It.IsAny<int>()))
            .ReturnsAsync(Invoice);

        var result = await _controller.GetInvoiceById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetInvoiceById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetInvoiceById(It.IsAny<int>()))
            .ReturnsAsync(Invoice);

        var result = await _controller.GetInvoiceById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateInvoice_ReturnsOk()
    {
        _service.Setup(s => s.CreateInvoice(It.IsAny<InvoiceModel>()))
            .ReturnsAsync(Invoice);

        var result = await _controller.CreateInvoice(Invoice);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateInvoice_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateInvoice(It.IsAny<InvoiceModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateInvoice(Invoice);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteInvoice_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteInvoice(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteInvoice(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteInvoice_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteInvoice(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteInvoice(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteInvoice_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteInvoice(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteInvoice(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}