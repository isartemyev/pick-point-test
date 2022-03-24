using AutoMapper;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Domain.Exceptions;
using PickPoint.Lib.Dto.Order;
using PickPoint.Lib.Facades.Declarations;
using PickPoint.Lib.Filters.Declarations;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Facades.Implementations;

public class OrderFacade : IOrderFacade
{
    private readonly IOrderFilter _filter;
    private readonly IMapper _mapper;
    private readonly IOrderRepository _repository;

    public OrderFacade(IOrderFilter filter, IMapper mapper, IOrderRepository repository)
    {
        _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<OrderDto> CreateAsync(OrderCreateDto payload, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var entity = new PickPointOrderEntity();

        _mapper.Map(payload, entity);

        await _repository.CreateAsync(entity, token);

        return _mapper.Map<OrderDto>(entity);
    }

    public async Task<OrderDto> ReadAsync(string id, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var entity = await _repository.ReadAsync(id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        return _mapper.Map<OrderDto>(entity);
    }

    public async Task UpdateAsync(OrderUpdateDto payload, PickPointMerchantEntity requester,
        CancellationToken token = default)
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

    public async Task<OrderDto[]> ListAsync(OrderFilterDto filter, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var all = await _repository.AllAsync(token);

        if (all is null)
        {
            return Array.Empty<OrderDto>();
        }

        var filtered = _filter.Apply(all, filter, requester)?.ToArray();

        if (filtered is null || !filtered.Any())
        {
            return Array.Empty<OrderDto>();
        }

        return _mapper.Map<OrderDto[]>(filtered);
    }

    public async Task CancelAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default)
    {
        if (requester.Role is EMerchantRole.None or EMerchantRole.Anonymous)
        {
            throw new PickPointAccessDeniedException();
        }

        var entity = await _repository.ReadAsync(id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        entity.Cancel();

        await _repository.UpdateAsync(entity, token);
    }
}