using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Dto.Merchant;

namespace PickPoint.Lib.Filters.Declarations;

public interface IMerchantFilter : IDataFilter<IQueryable<PickPointMerchantEntity>, MerchantFilterDto, PickPointMerchantEntity, IEnumerable<PickPointMerchantEntity>>
{
}