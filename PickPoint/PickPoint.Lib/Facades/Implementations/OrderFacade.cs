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
    private readonly IOrderRepository _orderRepository;
    private readonly IMachineRepository _machineRepository;

    public OrderFacade(IOrderFilter filter, IMapper mapper, IOrderRepository orderRepository, IMachineRepository machineRepository)
    {
        _filter = filter ?? throw new ArgumentNullException(nameof(filter));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _machineRepository = machineRepository ?? throw new ArgumentNullException(nameof(machineRepository));
    }

    public async Task<OrderDto> CreateAsync(OrderCreateDto payload, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var entity = new PickPointOrderEntity();

        _mapper.Map(payload, entity);

        var machine = await _machineRepository.FindAsync(item => item.Number.Equals(entity.MachineNumber), token);

        if (machine is null || !machine.Enabled)
        {
            throw new PickPointAccessDeniedException();
        }

        await _orderRepository.CreateAsync(entity, token);

        return _mapper.Map<OrderDto>(entity);
    }

    public async Task<OrderDto> ReadAsync(string id, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var entity = await _orderRepository.ReadAsync(id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        return _mapper.Map<OrderDto>(entity);
    }

    public async Task UpdateAsync(OrderUpdateDto payload, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var entity = await _orderRepository.ReadAsync(payload.Id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }
        
        var machine = await _machineRepository.FindAsync(item => item.Number.Equals(entity.MachineNumber), token);

        if (machine is null || !machine.Enabled)
        {
            throw new PickPointAccessDeniedException();
        }

        _mapper.Map(payload, entity);

        await _orderRepository.UpdateAsync(entity, token);
    }

    public async Task<bool> DeleteAsync(string id, PickPointMerchantEntity requester, CancellationToken token = default)
    {
        if (requester.Role != EMerchantRole.Admin)
        {
            throw new PickPointAccessDeniedException();
        }

        var entity = await _orderRepository.ReadAsync(id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        return await _orderRepository.DeleteAsync(id, token);
    }

    public async Task<OrderDto[]> ListAsync(OrderFilterDto filter, PickPointMerchantEntity requester,
        CancellationToken token = default)
    {
        var all = await _orderRepository.AllAsync(token);

        if (all is null)
        {
            return Array.Empty<OrderDto>();
        }

        var filtered = _filter.Apply(all, filter, requester).ToArray();

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

        var entity = await _orderRepository.ReadAsync(id, token);

        if (entity is null)
        {
            throw new PickPointEntityNotFoundException();
        }

        await _orderRepository.CancelAsync(id, token);
    }
}