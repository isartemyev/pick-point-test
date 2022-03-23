using AutoMapper;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Domain.Exceptions;
using PickPoint.Lib.Dto.Merchant;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Filters.Declarations;
using PickPoint.Lib.Helpers.Auth;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Facades.Implementations;

public class MerchantFacade : IMerchantFacade
{
    private readonly IMerchantFilter _filter;
    private readonly IMapper _mapper;
    private readonly IMerchantRepository _repository;

    public MerchantFacade(IMerchantFilter filter, IMapper mapper, IMerchantRepository repository)
    {
        _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<MerchantDto> CreateAsync(MerchantCreateDto payload, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        if (await CheckSameEmail(payload.Email, token) || await CheckSameEmail(payload.Login, token))
        {
            throw new PickPointValidationException("E-mail address is already in use.");
        }

        var entity = new PickPointMerchantEntity();

        _mapper.Map(payload, entity);

        entity.SetPasswordHash(PasswordHasher.Hash(payload.Password));

        await _repository.CreateAsync(entity, token);

        return _mapper.Map<MerchantDto>(entity);
    }

    public async Task<MerchantDto> ReadAsync(string id, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var entity = await _repository.ReadAsync(id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        return _mapper.Map<MerchantDto>(entity);
    }

    public async Task UpdateAsync(MerchantUpdateDto payload, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
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

    public async Task<MerchantDto[]> ListAsync(MerchantFilterDto filter, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var all = await _repository.AllAsync(token);

        if (all is null)
        {
            return Array.Empty<MerchantDto>();
        }

        var filtered = _filter.Apply(all, filter, requester)?.ToArray();

        if (filtered is null || !filtered.Any())
        {
            return Array.Empty<MerchantDto>();
        }

        return _mapper.Map<MerchantDto[]>(filtered);
    }

    private async Task<bool> CheckSameEmail(string email, CancellationToken token)
    {
        return (await _repository.FilterAsync(x => x.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase),
            token)).Any();
    }
}