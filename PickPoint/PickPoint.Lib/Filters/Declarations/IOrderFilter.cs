using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Dto.Order;

namespace PickPoint.Lib.Filters.Declarations;

public interface IOrderFilter : IDataFilter<IQueryable<PickPointOrderEntity>, OrderFilterDto, PickPointMerchantEntity, IEnumerable<PickPointOrderEntity>>
{
        
}