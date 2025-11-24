using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class PhoneControllerTests
{
    private PhoneModel Phone { get; } = new()
    {
        PhoneId = 1,
        CustomerId = 1,
        PhoneNumber = "PhoneNumber",
        PhoneType = "PhoneType"
    };

    private readonly Mock<IPhoneService> _service;
    private readonly PhoneController _controller;

    public PhoneControllerTests()
    {
        _service = new Mock<IPhoneService>();
        _controller = new PhoneController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic phone
        _service.Setup(s => s.GetAllPhones())
            .ReturnsAsync(new List<PhoneModel>());

        var result = await _controller.GetAllPhones();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetPhoneById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetPhoneById(It.IsAny<int>()))
            .ReturnsAsync(Phone);

        var result = await _controller.GetPhoneById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetPhoneById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetPhoneById(It.IsAny<int>()))
            .ReturnsAsync(Phone);

        var result = await _controller.GetPhoneById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreatePhone_ReturnsOk()
    {
        _service.Setup(s => s.CreatePhone(It.IsAny<PhoneModel>()))
            .ReturnsAsync(Phone);

        var result = await _controller.CreatePhone(Phone);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdatePhone_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdatePhone(It.IsAny<PhoneModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdatePhone(Phone);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeletePhone_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeletePhone(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeletePhone(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeletePhone_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeletePhone(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeletePhone(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeletePhone_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeletePhone(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeletePhone(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}