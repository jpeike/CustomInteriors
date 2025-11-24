using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class PaymentControllerTests
{
    private PaymentModel Payment { get; } = new()
    {
        InvoiceId = 1,
        PaymentId = 1,
        AmountPaid = 100.0m,
        Method = "Method",
        PaymentDate = DateTime.Now,
    };

    private readonly Mock<IPaymentService> _service;
    private readonly PaymentController _controller;

    public PaymentControllerTests()
    {
        _service = new Mock<IPaymentService>();
        _controller = new PaymentController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic payment
        _service.Setup(s => s.GetAllPayments())
            .ReturnsAsync(new List<PaymentModel>());

        var result = await _controller.GetAllPayments();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetPaymentById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetPaymentById(It.IsAny<int>()))
            .ReturnsAsync(Payment);

        var result = await _controller.GetPaymentById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetPaymentById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetPaymentById(It.IsAny<int>()))
            .ReturnsAsync(Payment);

        var result = await _controller.GetPaymentById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreatePayment_ReturnsOk()
    {
        _service.Setup(s => s.CreatePayment(It.IsAny<PaymentModel>()))
            .ReturnsAsync(Payment);

        var result = await _controller.CreatePayment(Payment);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdatePayment_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdatePayment(It.IsAny<PaymentModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdatePayment(Payment);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeletePayment_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeletePayment(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeletePayment(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeletePayment_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeletePayment(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeletePayment(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeletePayment_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeletePayment(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeletePayment(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}