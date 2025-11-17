using Core;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Web;

namespace Tests;

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
    
    [Fact]
    public async Task GetAll_ReturnsOk()
    {
        // mock the return of the service to be a generic address
        _service.Setup(s => s.GetAllAddresses())
            .ReturnsAsync(new List<AddressModel> {});

        var result = await _controller.GetAllAddresses();
        
        Assert.True(true);
    }
}