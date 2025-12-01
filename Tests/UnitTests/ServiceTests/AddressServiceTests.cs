using Core;
using Infrastructure;
using Moq;

namespace Testing.UnitTests.ServiceTests;


/// <summary>
/// testing services is not really worthwhile because it's mainly a compatability layer
/// for repositories. repositories should be integration tested because of the EF core connection
/// </summary>
public class AddressServiceTests
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
    
    private readonly Mock<IAddressRepository> _repository;
    private readonly IAddressService _service;
    
    public  AddressServiceTests()
    {
        _repository = new Mock<IAddressRepository>();
        _service = new AddressService(_repository.Object);
    }

    [Fact]
    public async Task GetAllAddresses_ReturnsSomething()
    {
        _repository.Setup(s => s.GetAllAddresses())
            .ReturnsAsync(new List<Address>());
        
        var result = await _service.GetAllAddresses();
        
        Assert.NotNull(result);
    }
    
    [Fact]
    public async Task GetAddressByCustomerId_ReturnsCustomer()
    {
        _repository.Setup(s => s.GetAddressesByCustomerId(It.IsAny<int>()))
            .ReturnsAsync(new List<Address>());
        
        var result = await _service.GetAddressesByCustomerId(It.IsAny<int>());
        
        Assert.NotNull(result);
    }
}