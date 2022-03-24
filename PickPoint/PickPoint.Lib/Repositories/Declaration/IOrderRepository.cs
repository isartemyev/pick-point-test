using PickPoint.Lib.Domain.Core.Order;

namespace PickPoint.Lib.Repositories.Declaration;

public interface IOrderRepository : IMongoRepository<PickPointOrderEntity>
{
    Task CancelAsync(string id, CancellationToken token = default);
}