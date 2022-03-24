using PickPoint.Lib.Domain.Core.Machine;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Filters.Extensions;

internal static class MachineFilterExtensions
{
    internal static IQueryable<PickPointMachineEntity> FilterByIds(this IQueryable<PickPointMachineEntity> data, IEnumerable<string> ids)
    {
        return ids is null ? data : data.Where(m => ids.Contains(m.Id));
    }

    internal static IQueryable<PickPointMachineEntity> FilterByTime(this IQueryable<PickPointMachineEntity> data, long? startTime = null, long? endTime = null)
    {
        if (startTime != null && endTime != null)
            return data.Where(m => m.CreatedAt >= startTime && m.CreatedAt <= endTime);

        if (startTime != null)
            return data.Where(m => m.CreatedAt >= startTime);

        if (endTime != null)
            return data.Where(m => m.CreatedAt <= endTime);

        return data;
    }
    
    internal static IQueryable<PickPointMachineEntity> FilterByEnabled(this IQueryable<PickPointMachineEntity> data, bool? flag)
    {
        return flag.HasValue ? data.Where(t => t.Enabled == flag.Value) : data;
    }

    internal static IEnumerable<PickPointMachineEntity> FilterByText(this IEnumerable<PickPointMachineEntity> data, string text)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text))
            return data;

        var searchWords = text.ToLower().Split(' ');

        data = data.Where(m => string.Join(' ', m.Number, m.Address).ToLower().ContainWordsStartsWithAll(searchWords));

        return data;
    }
}