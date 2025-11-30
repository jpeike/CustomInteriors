using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web.Controllers;

namespace Testing.UnitTests.ControllerTests;

/// <summary>
/// this class only unit tests the controller actions. services and model validation are tested elsewhere
/// </summary>
public class CustomerControllerTests
{
    private CustomerCreateModel CustomerCreateModel { get; } = new()
    {
        CompanyName = "CompanyName",
        CustomerNotes = "CustomerNotes",
        CustomerType = "CustomerType",
        FirstName = "FirstName",
        LastName = "LastName",
        PrefferedContactMethod = "PrefferedContactMethod",
        Status = "Status"
    };
    
    private CustomerUpdateModel CustomerUpdateModel { get; } = new()
    {
        CustomerId = 1,
        CompanyName = "CompanyName",
        CustomerNotes = "CustomerNotes",
        CustomerType = "CustomerType",
        FirstName = "FirstName",
        LastName = "LastName",
        PrefferedContactMethod = "PrefferedContactMethod",
        Status = "Status"
    };
    
    private CustomerModel CustomerModel { get; } = new()
    {
        CustomerId = 1,
        CompanyName = "CompanyName",
        CustomerNotes = "CustomerNotes",
        CustomerType = "CustomerType",
        FirstName = "FirstName",
        LastName = "LastName",
        PrefferedContactMethod = "PrefferedContactMethod",
        Status = "Status"
    };

    private readonly Mock<ICustomerService> _service;
    private readonly CustomersController _controller;

    public CustomerControllerTests()
    {
        _service = new Mock<ICustomerService>();
        _controller = new CustomersController(_service.Object);
    }

    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic customer
        _service.Setup(s => s.GetAllCustomers(It.IsAny<bool>()))
            .ReturnsAsync(new List<CustomerModel>());

        var result = await _controller.GetAllCustomers();

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetCustomerById_IdGreaterThanEqualsOne_ReturnsOk()
    {
        _service.Setup(s => s.GetCustomerById(It.IsAny<int>(), It.IsAny<bool>()))
            .ReturnsAsync(CustomerModel);

        var result = await _controller.GetCustomerById(1);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetCustomerById_IdLessThanOne_ReturnsBadRequest()
    {
        _service.Setup(s => s.GetCustomerById(It.IsAny<int>(), It.IsAny<bool>()))
            .ReturnsAsync(CustomerModel);

        var result = await _controller.GetCustomerById(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }

    [Fact]
    public async Task CreateCustomer_ReturnsOk()
    {
        _service.Setup(s => s.CreateCustomer(It.IsAny<CustomerCreateModel>()))
            .ReturnsAsync(CustomerModel);

        var result = await _controller.CreateCustomer(CustomerCreateModel);

        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task UpdateCustomer_ReturnsNoContent()
    {
        _service.Setup(s => s.UpdateCustomer(It.IsAny<CustomerUpdateModel>()))
            .Returns(Task.CompletedTask);

        var result = await _controller.UpdateCustomer(CustomerUpdateModel);

        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task DeleteCustomer_IdGreaterThanEqualOne_Exists_ReturnsOkTrue()
    {
        _service.Setup(s => s.DeleteCustomer(It.IsAny<int>()))
            .ReturnsAsync(true);

        var result = await _controller.DeleteCustomer(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.True(boolValue);
    }

    [Fact]
    public async Task DeleteCustomer_IdGreaterThanEqualOne_DoesNotExists_ReturnsOkFalse()
    {
        _service.Setup(s => s.DeleteCustomer(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteCustomer(1);
        Assert.IsType<OkObjectResult>(result.Result);

        var resultObj = result.Result as OkObjectResult;
        Assert.IsType<bool>(resultObj?.Value);

        var boolValue = (bool)resultObj.Value;
        Assert.False(boolValue);
    }

    [Fact]
    public async Task DeleteCustomer_IdLessThanOne_DoesNotExists_ReturnsBadRequest()
    {
        _service.Setup(s => s.DeleteCustomer(It.IsAny<int>()))
            .ReturnsAsync(false);

        var result = await _controller.DeleteCustomer(0);

        Assert.IsType<BadRequestResult>(result.Result);
    }
}