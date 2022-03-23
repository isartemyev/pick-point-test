using AutoMapper;
using PickPoint.Lib.Domain.Core.Machine;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Domain.Exceptions;
using PickPoint.Lib.Dto.Machine;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Filters.Declarations;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Facades.Implementations;

public class MachineFacade : IMachineFacade
{
    private readonly IMachineFilter _filter;
    private readonly IMapper _mapper;
    private readonly IMachineRepository _repository;

    public MachineFacade(IMachineFilter filter, IMapper mapper, IMachineRepository repository)
    {
        _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public async Task<MachineDto> CreateAsync(MachineCreateDto payload, PickPointMerchantEntity requester, CancellationToken token = default)
    {
        var entity = new PickPointMachineEntity();

        _mapper.Map(payload, entity);

        await _repository.CreateAsync(entity, token);

        return _mapper.Map<MachineDto>(entity);
    }

    public async Task<MachineDto> ReadAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default)
    {
        var entity = await _repository.ReadAsync(id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        return _mapper.Map<MachineDto>(entity);
    }

    public async Task UpdateAsync(MachineUpdateDto payload, PickPointMerchantEntity requester, CancellationToken token = default)
    {
        if (requester.Role != EMerchantRole.Admin)
        {
            throw new PickPointAccessDeniedException();
        }

        var entity = await _repository.ReadAsync(payload.Id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        _mapper.Map(payload, entity);

        await _repository.UpdateAsync(entity, token);
    }

    public async Task<bool> DeleteAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default)
    {
        if (requester.Role != EMerchantRole.Admin)
        {
            throw new PickPointAccessDeniedException();
        }

        var entity = await _repository.ReadAsync(id, token);

        if (entity is null)
        {
            return true;
        }

        return await _repository.DeleteAsync(id, token);
    }

    public async Task<MachineDto[]> ListAsync(MachineFilterDto filter, PickPointMerchantEntity requester, CancellationToken token = default)
    {
        var all = await _repository.AllAsync(token);

        if (all is null)
        {
            return Array.Empty<MachineDto>();
        }

        var filtered = _filter.Apply(all, filter, requester)?.ToArray();

        if (filtered is null || !filtered.Any())
        {
            return Array.Empty<MachineDto>();
        }

        return _mapper.Map<MachineDto[]>(filtered);
    }
}