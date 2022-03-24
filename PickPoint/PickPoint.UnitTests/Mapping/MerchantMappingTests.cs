using AutoMapper;
using NUnit.Framework;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Dto.Merchant;
using PickPoint.Lib.Mapping;

namespace PickPoint.UnitTests.Mapping;

[TestFixture]
public class MerchantMappingTests
{
    private IMapper _mapper;
    
    [SetUp]
    public void SetUp()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(MerchantMappingProfile.Init());
        });
        
        _mapper = mappingConfig.CreateMapper();
    }
    
    [Test]
    public void DtoToDomain_CreateDto_ShouldBeEquals()
    {
        // arrange
        var dto = new MerchantCreateDto
        {
            Login = "Test address",
            Role = EMerchantRole.Merchant,
            Email = "user@example.com",
            Name = "user"
        };

        // act
        var actual = new PickPointMerchantEntity();
        _mapper.Map(dto, actual);

        // assert
        Assert.AreEqual(dto.Login, actual.Login);
        Assert.AreEqual(dto.Role, actual.Role);
        Assert.AreEqual(dto.Email, actual.Email);
        Assert.AreEqual(dto.Name, actual.Name);
    }
    
    [Test]
    public void DtoToDomain_UpdateDto_ShouldBeEquals()
    {
        // arrange
        var dto = new MerchantUpdateDto
        {
            Id = "a8dca85d601145b6bae87e5da1789ecf",
            Email = "user@example.com",
            Name = "user"
        };

        // act
        var actual = new PickPointMerchantEntity();
        _mapper.Map(dto, actual);

        // assert
        Assert.AreEqual(dto.Id, actual.Id);
        Assert.AreEqual(dto.Email, actual.Email);
        Assert.AreEqual(dto.Name, actual.Name);
    }
    
    [Test]
    public void DomainToDto_Entity_ShouldBeEquals()
    {
        // arrange
        var entity = new PickPointMerchantEntity("user", "user@example.com", EMerchantRole.Merchant, "user@example.com");

        // act
        var actual = new MerchantDto();
        _mapper.Map(entity, actual);

        // assert
        Assert.AreEqual(entity.Id, actual.Id);
        Assert.AreEqual(entity.CreatedAt, actual.CreatedAt);
        Assert.AreEqual(entity.UpdatedAt, actual.UpdatedAt);
        Assert.AreEqual(entity.Name, actual.Name);
        Assert.AreEqual(entity.Login, actual.Login);
        Assert.AreEqual(entity.Role, actual.Role);
        Assert.AreEqual(entity.Email, actual.Email);
    }
}