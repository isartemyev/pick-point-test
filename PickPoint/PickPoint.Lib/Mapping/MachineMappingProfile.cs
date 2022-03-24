using AutoMapper;
using PickPoint.Lib.Domain.Core.Machine;
using PickPoint.Lib.Dto.Machine;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Mapping;

public class MachineMappingProfile : Profile
{
    public static MachineMappingProfile Init() => new MachineMappingProfile().CreateMap();

    private MachineMappingProfile CreateMap()
    {
        DomainToDtoMap();
        DtoToDomainMap();

        return this;
    }

    private void DtoToDomainMap()
    {
        CreateMap<MachineCreateDto, PickPointMachineEntity>();

        CreateMap<MachineUpdateDto, PickPointMachineEntity>()
            .ForMember(dst => dst.Enabled, opt =>
                opt.MapFrom(src => src.Enabled!.Value))
            .ForMember(dst => dst.UpdatedAt, opt =>
                opt.MapFrom(src => DateTime.UtcNow.ToUnixTimeMilliseconds()));
    }

    private void DomainToDtoMap()
    {
        CreateMap<PickPointMachineEntity, MachineDto>();
    }
}