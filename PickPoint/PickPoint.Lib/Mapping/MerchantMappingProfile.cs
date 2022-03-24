using AutoMapper;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Dto.Merchant;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Mapping;

public class MerchantMappingProfile : Profile
{
    public static MerchantMappingProfile Init() => new MerchantMappingProfile().CreateMap();

    private MerchantMappingProfile CreateMap()
    {
        DomainToDtoMap();
        DtoToDomainMap();

        return this;
    }

    private void DtoToDomainMap()
    {
        CreateMap<MerchantCreateDto, PickPointMerchantEntity>();

        CreateMap<MerchantUpdateDto, PickPointMerchantEntity>()
            .ForMember(dst => dst.UpdatedAt, opt =>
                opt.MapFrom(src => DateTime.UtcNow.ToUnixTimeMilliseconds()));
    }

    private void DomainToDtoMap()
    {
        CreateMap<PickPointMerchantEntity, MerchantDto>();
    }
}