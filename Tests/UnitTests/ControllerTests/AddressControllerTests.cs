using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class AddressControllerTests
{
    private AddressModel Address { get; } = new()
    {
        AddressId = 1,
        AddressType = "string",
        City = "string",
        Country = "string",
        CustomerId = 1,
        PostalCode = 11111,
        State = "string",
        Street = "string",
    };

    private readonly Mock<IAddressService> _service;
    private readonly AddressController _controller;

    public AddressControllerTests()
    {
        _service = new Mock<IAddressService>();
        _controller = new AddressController(_service.Object);
    }

    /// <summary>
    /// GetAll should always return ok if the endpoint it up
    /// </summary>
    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic address
        _service.Setup(s => s.GetAllAddresses())
            .ReturnsAsync(new List<AddressModel>());

        var result = await _controller.GetAllAddresses();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAddressesByCustomerId_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetAddressesByCustomerId(It.IsAny<int>()))
            .ReturnsAsync(new List<AddressModel>());

        var result = await _controller.GetAddressesByCustomerId(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetAddressesByCustomerId_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetAddressesByCustomerId(It.IsAny<int>()))
            .ReturnsAsync(new List<AddressModel>());

        var result = await _controller.GetAddressesByCustomerId(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateAddress_ReturnsOk()
    {
        _service.Setup(s => s.CreateAddress(It.IsAny<AddressModel>()))
            .ReturnsAsync(Address);

        var result = await _controller.CreateAddress(Address);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    // Cannot test action model validation through unit tests because the model validation
    // middleware is not active in the unit testing environment. to test this it needs
    // to be through integration tests where the whole backend stack is active

    // [Fact]
    // public async Task CreateAddress_InvalidPostal_ReturnsBadRequest()
    // {
    //     _service.Setup(s => s.CreateAddress(It.IsAny<AddressModel>()))
    //         .ReturnsAsync(Address);
    //
    //     AddressModel newAddress = Address;
    //     newAddress.PostalCode = 0;
    //     newAddress.CustomerId = 0;
    //
    //     var result = await _controller.CreateAddress(newAddress);
    //     
    //     Assert.IsType<BadRequestResult>(result.Result);
    // }

    [Fact]
    public async Task UpdateAddress_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateAddress(It.IsAny<AddressModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateAddress(Address);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteAddress_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteAddress(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteAddress(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteAddress_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteAddress(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteAddress(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteAddress_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteAddress(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteAddress(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}