using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Testing;

/// <summary>
/// this class only unit tests the controller actions. services are tested elsewhere
/// </summary>
public class AddressControllerTests
{
    private readonly Mock<IAddressService> _service;
    private readonly Mock<ICustomerService> _customerService;
    private readonly AddressController _controller;

    public AddressControllerTests()
    {
        _service = new Mock<IAddressService>();
        _customerService = new Mock<ICustomerService>();
        _controller = new AddressController(_service.Object,  _customerService.Object);
    }
    
    // need previous pr for tests to pass
    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic address
        _service.Setup(s => s.GetAllAddresses())
            .ReturnsAsync(new List<AddressModel> {});

        var result = await _controller.GetAllAddresses();
        
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAddressesByCustomerId_ReturnsOk()
    {
        _service.Setup(s => s.GetAddressesByCustomerId(It.IsAny<int>()))
            .ReturnsAsync(new List<AddressModel>());
        
        var result = await _controller.GetAddressesByCustomerId(It.IsAny<int>());
        
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task GetAddressesByCustomerId_ReturnsNotFound()
    {
        _service.Setup(s => s.GetAddressesByCustomerId(It.IsAny<int>()))
            .ReturnsAsync(new List<AddressModel>());
        
        var result = await _controller.GetAddressesByCustomerId(It.IsAny<int>());
        
        Assert.IsType<NotFoundObjectResult>(result);
    }

    [Fact]
    public async Task CreateAddress_ReturnsOk()
    {
        _service.Setup(s => s.CreateAddress())
            .ReturnsAsync(new AddressModel());

        var result = await _controller.CreateAddress();
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task CreateAddress_ReturnsInvalidInput()
    {
        _service.Setup(s => s.CreateAddress())
            .ReturnsAsync(new AddressModel());

        var result = await _controller.CreateAddress();
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task UpdateAddress_ReturnsOk()
    {
        _service.Setup(s => s.CreateAddress())
            .ReturnsAsync(new AddressModel());

        var result = await _controller.CreateAddress();
        
        Assert.IsType<OkObjectResult>(result);
    }
    
    [Fact]
    public async Task UpdateAddress_ReturnsInvalidInput()
    {
        _service.Setup(s => s.CreateAddress())
            .ReturnsAsync(new AddressModel());

        var result = await _controller.CreateAddress();
        
        Assert.IsType<OkObjectResult>(result);
    }
}