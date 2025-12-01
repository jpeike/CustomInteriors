using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class UserControllerTests
{
    private UserModel User { get; } = new()
    {
        Id = 1,
        CustomerId = 1,
        CreatedOn = DateTime.Now,
        Email = "Email@email.com",
        Username = "Username",
    };

    private readonly Mock<IUserService> _service;
    private readonly UsersController _controller;

    public UserControllerTests()
    {
        _service = new Mock<IUserService>();
        _controller = new UsersController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic user
        _service.Setup(s => s.GetAllUsers())
            .ReturnsAsync(new List<UserModel>());

        var result = await _controller.GetAllUsers();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetUserById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(User);

        var result = await _controller.GetUserById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetUserById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetUserById(It.IsAny<int>()))
            .ReturnsAsync(User);

        var result = await _controller.GetUserById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateUser_ReturnsOk()
    {
        _service.Setup(s => s.CreateUser(It.IsAny<UserModel>()))
            .ReturnsAsync(User);

        var result = await _controller.CreateUser(User);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateUser_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateUser(It.IsAny<UserModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateUser(User);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteUser_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteUser(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteUser(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteUser_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteUser(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteUser(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteUser_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteUser(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteUser(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}