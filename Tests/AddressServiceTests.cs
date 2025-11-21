using Core;
using Infrastructure;
using Moq;

namespace Testing;

public class AddressServiceTests
{
    private readonly Mock<IAddressRepository> _repository;
    private readonly IAddressService _service;
    
    public  AddressServiceTests()
    {
        _repository = new Mock<IAddressRepository>();
        _service = new AddressService(_repository.Object);
    }
}