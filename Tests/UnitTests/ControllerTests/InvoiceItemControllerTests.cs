using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class InvoiceItemControllerTests
{
    private InvoiceItemModel InvoiceItem { get; } = new()
    {
        InvoiceId = 1,
        Amount = 123,
        Description = "Description",
        ItemId = 1,
        Price = 123.45m,
    };

    private readonly Mock<IInvoiceItemService> _service;
    private readonly InvoiceItemController _controller;

    public InvoiceItemControllerTests()
    {
        _service = new Mock<IInvoiceItemService>();
        _controller = new InvoiceItemController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic invoiceItem
        _service.Setup(s => s.GetAllInvoiceItems())
            .ReturnsAsync(new List<InvoiceItemModel>());

        var result = await _controller.GetAllInvoiceItems();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetInvoiceItemById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetInvoiceItemById(It.IsAny<int>()))
            .ReturnsAsync(InvoiceItem);

        var result = await _controller.GetInvoiceItemById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetInvoiceItemById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetInvoiceItemById(It.IsAny<int>()))
            .ReturnsAsync(InvoiceItem);

        var result = await _controller.GetInvoiceItemById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateInvoiceItem_ReturnsOk()
    {
        _service.Setup(s => s.CreateInvoiceItem(It.IsAny<InvoiceItemModel>()))
            .ReturnsAsync(InvoiceItem);

        var result = await _controller.CreateInvoiceItem(InvoiceItem);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateInvoiceItem_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateInvoiceItem(It.IsAny<InvoiceItemModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateInvoiceItem(InvoiceItem);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteInvoiceItem_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteInvoiceItem(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteInvoiceItem(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteInvoiceItem_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteInvoiceItem(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteInvoiceItem(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteInvoiceItem_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteInvoiceItem(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteInvoiceItem(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}