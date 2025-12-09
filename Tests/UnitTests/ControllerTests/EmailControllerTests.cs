using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class EmailControllerTests
{
    private EmailModel Email { get; } = new()
    {
        EmailId = 1,
        //CreatedOn = DateTime.Now,
        CustomerId = 1,
        EmailAddress = "Email@Address.com",
        EmailType = "EmailType",
    };

    private readonly Mock<IEmailService> _service;
    private readonly EmailsController _controller;

    public EmailControllerTests()
    {
        _service = new Mock<IEmailService>();
        _controller = new EmailsController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic email
        _service.Setup(s => s.GetAllEmails())
            .ReturnsAsync(new List<EmailModel>());

        var result = await _controller.GetAllEmails();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetEmailById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetEmailById(It.IsAny<int>()))
            .ReturnsAsync(Email);

        var result = await _controller.GetEmailById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetEmailById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetEmailById(It.IsAny<int>()))
            .ReturnsAsync(Email);

        var result = await _controller.GetEmailById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateEmail_ReturnsOk()
    {
        _service.Setup(s => s.CreateEmail(It.IsAny<EmailModel>()))
            .ReturnsAsync(Email);

        var result = await _controller.CreateEmail(Email);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateEmail_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateEmail(It.IsAny<EmailModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateEmail(Email);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteEmail_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteEmail(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteEmail(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteEmail_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteEmail(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteEmail(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteEmail_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteEmail(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteEmail(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}