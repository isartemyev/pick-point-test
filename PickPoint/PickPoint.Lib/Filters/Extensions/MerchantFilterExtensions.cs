using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Extensions;

namespace PickPoint.Lib.Filters.Extensions;

internal static class MerchantFilterExtensions
{
    internal static IQueryable<PickPointMerchantEntity> FilterByIds(this IQueryable<PickPointMerchantEntity> data, IEnumerable<string>? ids)
    {
        return ids is null ? data : data.Where(m => ids.Contains(m.Id));
    }

    internal static IQueryable<PickPointMerchantEntity> FilterByTime(this IQueryable<PickPointMerchantEntity> data, long? startTime = null, long? endTime = null)
    {
        if (startTime != null && endTime != null)
            return data.Where(m => m.CreatedAt >= startTime && m.CreatedAt <= endTime);

        if (startTime != null)
            return data.Where(m => m.CreatedAt >= startTime);

        if (endTime != null)
            return data.Where(m => m.CreatedAt <= endTime);

        return data;
    }
    
    internal static IQueryable<PickPointMerchantEntity> FilterByRole(this IQueryable<PickPointMerchantEntity> data, EMerchantRole? role)
    {
        return role.HasValue ? data.Where(t => t.Role == role.Value) : data;
    }

    internal static IEnumerable<PickPointMerchantEntity> FilterByText(this IEnumerable<PickPointMerchantEntity> data, string? text)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrEmpty(text))
            return data;

        var searchWords = text.ToLower().Split(' ');

        data = data.Where(m => string.Join(' ', m.Name, m.Login, m.Email).ToLower().ContainWordsStartsWithAll(searchWords));

        return data;
    }
}