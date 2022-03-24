using AutoMapper;
using NUnit.Framework;
using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Dto.Order;
using PickPoint.Lib.Mapping;

namespace PickPoint.UnitTests.Mapping;

[TestFixture]
public class OrderMappingTests
{
    private IMapper _mapper;

    [SetUp]
    public void SetUp()
    {
        var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(OrderMappingProfile.Init()); });

        _mapper = mappingConfig.CreateMapper();
    }

    [Test]
    public void DtoToDomain_CreateDto_ShouldBeEquals()
    {
        // arrange
        var dto = new OrderCreateDto
        {
            Number = 123,
            Items = new[] {"item1", "item2"},
            Amount = 1000,
            MachineNumber = "1234-567",
            RecipientName = "Customer",
            RecipientPhone = "+7999-888-77-66"
        };

        // act
        var actual = new PickPointOrderEntity();
        _mapper.Map(dto, actual);

        // assert
        Assert.AreEqual(dto.Number, actual.Number);
        Assert.AreEqual(dto.Items, actual.Items);
        Assert.AreEqual(dto.Amount, actual.Amount);
        Assert.AreEqual(dto.MachineNumber, actual.MachineNumber);
        Assert.AreEqual(dto.RecipientName, actual.RecipientName);
        Assert.AreEqual(dto.RecipientPhone, actual.RecipientPhone);
    }

    [Test]
    public void DtoToDomain_UpdateDto_ShouldBeEquals()
    {
        // arrange
        var dto = new OrderUpdateDto
        {
            Id = "a8dca85d601145b6bae87e5da1789ecf",
            Number = 123,
            Items = new[] {"item1", "item2"},
            Amount = 1000,
            RecipientName = "Customer",
            RecipientPhone = "+7999-888-77-66"
        };

        // act
        var actual = new PickPointOrderEntity();
        _mapper.Map(dto, actual);

        // assert
        Assert.AreEqual(dto.Id, actual.Id);
        Assert.AreEqual(dto.Number, actual.Number);
        Assert.AreEqual(dto.Items, actual.Items);
        Assert.AreEqual(dto.Amount, actual.Amount);
        Assert.AreEqual(dto.RecipientName, actual.RecipientName);
        Assert.AreEqual(dto.RecipientPhone, actual.RecipientPhone);
    }

    [Test]
    public void DomainToDto_Entity_ShouldBeEquals()
    {
        // arrange
        var entity = new PickPointOrderEntity(123, EOrderStatus.Registered, new[] {"item1", "item2"}, 1000, "1234-567",
            "+7999-888-77-66", "Customer");

        // act
        var actual = new OrderDto();
        _mapper.Map(entity, actual);

        // assert
        Assert.AreEqual(entity.Id, actual.Id);
        Assert.AreEqual(entity.CreatedAt, actual.CreatedAt);
        Assert.AreEqual(entity.UpdatedAt, actual.UpdatedAt);
        Assert.AreEqual(entity.Number, actual.Number);
        Assert.AreEqual(entity.Status, actual.Status);
        Assert.AreEqual(entity.Items, actual.Items);
        Assert.AreEqual(entity.Amount, actual.Amount);
        Assert.AreEqual(entity.MachineNumber, actual.MachineNumber);
        Assert.AreEqual(entity.RecipientPhone, actual.RecipientPhone);
        Assert.AreEqual(entity.RecipientName, actual.RecipientName);
    }
}