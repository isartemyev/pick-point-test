using MongoDB.Driver;
using PickPoint.Lib.Contexts;
using PickPoint.Lib.Domain.Core.Order;
using PickPoint.Lib.Domain.Enums;
using PickPoint.Lib.Extensions;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Repositories.Implementation;

public class OrderRepository : MongoRepository<PickPointOrderEntity>, IOrderRepository
{
    private const string CollectionName = "orders";

    public OrderRepository(MongoContext context) : base(context)
    {
        Entities = DbContext.DataBase.GetCollection<PickPointOrderEntity>(CollectionName);
    }

    public async Task CancelAsync(string id, CancellationToken token = default)
    {
        var filter = Builders<PickPointOrderEntity>.Filter.Eq(item => item.Id, id);
        
        var update = Builders<PickPointOrderEntity>.Update
            .Set(item => item.Status, EOrderStatus.Canceled)
            .Set(item => item.UpdatedAt, DateTime.UtcNow.ToUnixTimeMilliseconds())
            ; 
        
        var result = await Entities.UpdateOneAsync(filter, update, cancellationToken: token);

        Console.WriteLine(result);
    }
}