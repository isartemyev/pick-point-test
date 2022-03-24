using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Domain.Exceptions;
using PickPoint.Lib.Dto.Merchant;
using PickPoint.Lib.Filters.Declarations;
using PickPoint.Lib.Filters.Extensions;

namespace PickPoint.Lib.Filters.Implementations;

public class MerchantFilter : IMerchantFilter
{
    public IEnumerable<PickPointMerchantEntity> Apply(IQueryable<PickPointMerchantEntity> data, MerchantFilterDto filter, PickPointMerchantEntity option)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        if (filter is null)
        {
            throw new ArgumentNullException(nameof(filter));
        }

        if (option.Role != EMerchantRole.Admin)
        {
            throw new PickPointAccessDeniedException();
        }

        var filtered = data
            .FilterByIds(filter.Ids)
            .FilterByRole(filter.Role)
            .FilterByTime(filter.PeriodStart, filter.PeriodEnd)
            .AsEnumerable()
            .FilterByText(filter.Text);

        return filtered.ToArray();
    }
}