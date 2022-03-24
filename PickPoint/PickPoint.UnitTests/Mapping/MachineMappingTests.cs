using AutoMapper;
using NUnit.Framework;
using PickPoint.Lib.Domain.Core.Machine;
using PickPoint.Lib.Dto.Machine;
using PickPoint.Lib.Mapping;

namespace PickPoint.UnitTests.Mapping;

[TestFixture]
public class MachineMappingTests
{
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(OrderMappingProfile.Init());
            mc.AddProfile(MachineMappingProfile.Init());
            mc.AddProfile(MerchantMappingProfile.Init());
        });
        
        _mapper = mappingConfig.CreateMapper();
    }
    
    [Test]
    public void DtoToDomain_CreateDto_ShouldBeEquals()
    {
        // arrange
        var dto = new MachineCreateDto
        {
            Address = "Test address",
            Number = "1234-567"
        };

        // act
        var actual = new PickPointMachineEntity();
        _mapper.Map(dto, actual);

        // assert
        Assert.AreEqual(dto.Address, actual.Address);
        Assert.AreEqual(dto.Number, actual.Number);
    }
    
    [Test]
    public void DtoToDomain_UpdateDto_ShouldBeEquals()
    {
        // arrange
        var dto = new MachineUpdateDto
        {
            Id = "a8dca85d601145b6bae87e5da1789ecf",
            Address = "Test address",
            Number = "1234-567",
            Enabled = true
        };

        // act
        var actual = new PickPointMachineEntity();
        _mapper.Map(dto, actual);

        // assert
        Assert.AreEqual(dto.Id, actual.Id);
        Assert.AreEqual(dto.Address, actual.Address);
        Assert.AreEqual(dto.Number, actual.Number);
        Assert.AreEqual(dto.Enabled, actual.Enabled);
    }
    
    [Test]
    public void DomainToDto_Entity_ShouldBeEquals()
    {
        // arrange
        var entity = new PickPointMachineEntity("1234-567", "Test address", true);

        // act
        var actual = new MachineDto();
        _mapper.Map(entity, actual);

        // assert
        Assert.AreEqual(entity.Id, actual.Id);
        Assert.AreEqual(entity.CreatedAt, actual.CreatedAt);
        Assert.AreEqual(entity.UpdatedAt, actual.UpdatedAt);
        Assert.AreEqual(entity.Address, actual.Address);
        Assert.AreEqual(entity.Number, actual.Number);
        Assert.AreEqual(entity.Enabled, actual.Enabled);
    }
}