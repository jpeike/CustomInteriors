using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class QuoteRequestControllerTests
{
    private QuoteRequestModel QuoteRequest { get; } = new()
    {
        JobId = 1,
        QuoteId = 1,
        DescriptionOfWork = "DescriptionOfWork",
        EstimatedPrice = 100.0m,
        RequestDate = DateTime.Now,
    };

    private readonly Mock<IQuoteRequestService> _service;
    private readonly QuoteRequestController _controller;

    public QuoteRequestControllerTests()
    {
        _service = new Mock<IQuoteRequestService>();
        _controller = new QuoteRequestController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic quoteRequest
        _service.Setup(s => s.GetAllQuoteRequests())
            .ReturnsAsync(new List<QuoteRequestModel>());

        var result = await _controller.GetAllQuoteRequests();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetQuoteRequestById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetQuoteRequestById(It.IsAny<int>()))
            .ReturnsAsync(QuoteRequest);

        var result = await _controller.GetQuoteRequestById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetQuoteRequestById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetQuoteRequestById(It.IsAny<int>()))
            .ReturnsAsync(QuoteRequest);

        var result = await _controller.GetQuoteRequestById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateQuoteRequest_ReturnsOk()
    {
        _service.Setup(s => s.CreateQuoteRequest(It.IsAny<QuoteRequestModel>()))
            .ReturnsAsync(QuoteRequest);

        var result = await _controller.CreateQuoteRequest(QuoteRequest);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateQuoteRequest_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateQuoteRequest(It.IsAny<QuoteRequestModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateQuoteRequest(QuoteRequest);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteQuoteRequest_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteQuoteRequest(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteQuoteRequest(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteQuoteRequest_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteQuoteRequest(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteQuoteRequest(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteQuoteRequest_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteQuoteRequest(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteQuoteRequest(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}