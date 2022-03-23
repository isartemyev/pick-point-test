using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Dto.Order;

namespace PickPoint.Lib.Facades.Declarations;

public interface IOrderFacade
{
    Task<OrderDto> CreateAsync(OrderCreateDto payload, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<OrderDto> ReadAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default);

    Task UpdateAsync(OrderUpdateDto payload, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<bool> DeleteAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<OrderDto[]> ListAsync(OrderFilterDto filter, PickPointMerchantEntity requester, CancellationToken token = default);
}