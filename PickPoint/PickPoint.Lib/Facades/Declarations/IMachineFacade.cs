using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Dto.Machine;

namespace PickPoint.Lib.Facades.Declarations;

public interface IMachineFacade
{
    Task<MachineDto> CreateAsync(MachineCreateDto payload, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<MachineDto> ReadAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default);

    Task UpdateAsync(MachineUpdateDto payload, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<bool> DeleteAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default);

    Task<MachineDto[]> ListAsync(MachineFilterDto filter, PickPointMerchantEntity requester, CancellationToken token = default);
}