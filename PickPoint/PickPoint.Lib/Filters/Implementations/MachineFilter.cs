using PickPoint.Lib.Domain.Core.Machine;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Dto.Machine;
using PickPoint.Lib.Filters.Declarations;
using PickPoint.Lib.Filters.Extensions;

namespace PickPoint.Lib.Filters.Implementations;

public class MachineFilter : IMachineFilter
{
    public IEnumerable<PickPointMachineEntity> Apply(IQueryable<PickPointMachineEntity> data, MachineFilterDto filter,
        PickPointMerchantEntity option)
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
            .FilterByEnabled(filter.Enabled)
            .FilterByTime(filter.PeriodStart, filter.PeriodEnd)
            .OrderBy(i => i.Number)
            .AsEnumerable()
            .FilterByText(filter.Text)
            ;

        return filtered.ToArray();
    }
}