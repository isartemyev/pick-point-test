using PickPoint.Lib.Domain.Core.Machine;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Dto.Machine;

namespace PickPoint.Lib.Filters.Declarations;

public interface IMachineFilter: IDataFilter<IQueryable<PickPointMachineEntity>, MachineFilterDto, PickPointMerchantEntity, IEnumerable<PickPointMachineEntity>>
{
    
}