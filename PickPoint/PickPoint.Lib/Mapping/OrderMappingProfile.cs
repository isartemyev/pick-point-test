using AutoMapper;
using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Dto.Order;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Mapping;

public class OrderMappingProfile : Profile
{
    public static OrderMappingProfile Init() => new OrderMappingProfile().CreateMap();

    private OrderMappingProfile CreateMap()
    {
        DomainToDtoMap();
        DtoToDomainMap();
            
        return this;
    }

    private void DtoToDomainMap()
    {
        CreateMap<OrderCreateDto, PickPointOrderEntity>()
            .ForMember(dst => dst.Amount, opt => 
                opt.MapFrom(src => src.Amount!.Value));

        CreateMap<OrderUpdateDto, PickPointOrderEntity>()
            .ForMember(dst => dst.Amount, opt => 
                opt.MapFrom(src => src.Amount!.Value))
            .ForMember(dst => dst.UpdatedAt, opt => 
                opt.MapFrom(src => DateTime.UtcNow.ToUnixTimeMilliseconds()));
    }

    private void DomainToDtoMap()
    {
        CreateMap<PickPointOrderEntity, OrderDto>();
    }
}