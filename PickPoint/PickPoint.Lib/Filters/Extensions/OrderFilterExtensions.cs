using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Filters.Extensions;

internal static class OrderFilterExtensions
{
    internal static IQueryable<PickPointOrderEntity> FilterByIds(this IQueryable<PickPointOrderEntity> data, IEnumerable<string> ids)
    {
        return ids is null ? data : data.Where(m => ids.Contains(m.Id));
    }
        
    internal static IQueryable<PickPointOrderEntity> FilterByStatus(this IQueryable<PickPointOrderEntity> data, EOrderStatus? status)
    {
        return status.HasValue ? data.Where(t => t.Status == status.Value) : data;
    }

    internal static IQueryable<PickPointOrderEntity> FilterByTime(this IQueryable<PickPointOrderEntity> data, long? startTime = null, long? endTime = null)
    {
        if (startTime != null && endTime != null)
            return data.Where(m => m.CreatedAt >= startTime && m.CreatedAt <= endTime);

        if (startTime != null)
            return data.Where(m => m.CreatedAt >= startTime);

        if (endTime != null)
            return data.Where(m => m.CreatedAt <= endTime);

        return data;
    }

    internal static IEnumerable<PickPointOrderEntity> FilterByText(this IEnumerable<PickPointOrderEntity> data, string text)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text))
            return data;

        var searchWords = text.ToLower().Split(' ');

        data = data.Where(m => string.Join(' ', m.MachineNumber, m.RecipientPhone, m.RecipientName).ToLower().ContainWordsStartsWithAll(searchWords));

        return data;
    }
}