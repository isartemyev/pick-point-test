using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Dto.Merchant;

namespace PickPoint.Lib.Facades.Declarations;

public interface IMerchantFacade
{
    Task<MerchantDto> CreateAsync(MerchantCreateDto payload, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<MerchantDto> ReadAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default);

    Task UpdateAsync(MerchantUpdateDto payload, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<bool> DeleteAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<MerchantDto[]> ListAsync(MerchantFilterDto filter, PickPointMerchantEntity requester, CancellationToken token = default);
}