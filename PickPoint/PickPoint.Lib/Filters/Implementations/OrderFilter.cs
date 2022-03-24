using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Dto.Order;
using PickPoint.Lib.Filters.Declarations;
using PickPoint.Lib.Filters.Extensions;

namespace PickPoint.Lib.Filters.Implementations;

public class OrderFilter : IOrderFilter
{
    public IEnumerable<PickPointOrderEntity> Apply(IQueryable<PickPointOrderEntity> data, OrderFilterDto filter, PickPointMerchantEntity option)
    {
        if (data is null)
        {
            throw new ArgumentNullException(nameof(data));
        }

        if (filter is null)
        {
            throw new ArgumentNullException(nameof(filter));
        }

        var filtered = data
            .FilterByIds(filter.Ids)
            .FilterByStatus(filter.Status)
            .FilterByTime(filter.PeriodStart, filter.PeriodEnd)
            .AsEnumerable()
            .FilterByText(filter.Text);

        return filtered.ToArray();
    }
}