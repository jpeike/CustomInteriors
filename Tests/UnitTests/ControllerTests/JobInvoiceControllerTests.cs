using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class JobInvoiceControllerTests
{
    private JobInvoiceModel JobInvoice { get; } = new()
    {
        InvoiceId = 1,
        JobId = 1,
        CreatedDate = DateTime.Now,
        PercentageOfInvoice = 50.0f,
    };

    private readonly Mock<IJobInvoiceService> _service;
    private readonly JobInvoiceController _controller;

    public JobInvoiceControllerTests()
    {
        _service = new Mock<IJobInvoiceService>();
        _controller = new JobInvoiceController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic jobInvoice
        _service.Setup(s => s.GetAllJobInvoices())
            .ReturnsAsync(new List<JobInvoiceModel>());

        var result = await _controller.GetAllJobInvoices();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetJobInvoiceById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetJobInvoiceById(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(JobInvoice);

        var result = await _controller.GetJobInvoiceById(1, 1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetJobInvoiceById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetJobInvoiceById(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(JobInvoice);

        var result = await _controller.GetJobInvoiceById(0, 0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateJobInvoice_ReturnsOk()
    {
        _service.Setup(s => s.CreateJobInvoice(It.IsAny<JobInvoiceModel>()))
            .ReturnsAsync(JobInvoice);

        var result = await _controller.CreateJobInvoice(JobInvoice);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateJobInvoice_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateJobInvoice(It.IsAny<JobInvoiceModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateJobInvoice(JobInvoice);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteJobInvoice_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteJobInvoice(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteJobInvoice(1, 1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteJobInvoice_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteJobInvoice(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteJobInvoice(1, 1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteJobInvoice_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteJobInvoice(It.IsAny<int>(), It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteJobInvoice(0, 0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}