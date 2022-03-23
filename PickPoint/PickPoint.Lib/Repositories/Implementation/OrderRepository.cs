using PickPoint.Lib.Contexts;
using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Repositories.Implementation;

public class OrderRepository : MongoRepository<PickPointOrderEntity>, IOrderRepository
{
    private const string CollectionName = "orders";

    public OrderRepository(MongoContext context) : base(context)
    {
        Entities = DbContext.DataBase.GetCollection<PickPointOrderEntity>(CollectionName);
    }
}