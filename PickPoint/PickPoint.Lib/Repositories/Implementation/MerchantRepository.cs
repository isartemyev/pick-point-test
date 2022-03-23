using PickPoint.Lib.Contexts;
using PickPoint.Lib.Domain.Core.Merchant;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Repositories.Implementation;

public class MerchantRepository : MongoRepository<PickPointMerchantEntity>, IMerchantRepository
{
    private const string CollectionName = "merchants";

    public MerchantRepository(MongoContext context) : base(context)
    {
        Entities = DbContext.DataBase.GetCollection<PickPointMerchantEntity>(CollectionName);
    }
}