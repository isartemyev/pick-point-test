using PickPoint.Lib.Contexts;
using PickPoint.Lib.Domain.Core.Machine;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Repositories.Implementation;

public class MachineRepository : MongoRepository<PickPointMachineEntity>, IMachineRepository
{
    private const string CollectionName = "machines";

    public MachineRepository(MongoContext context) : base(context)
    {
        Entities = DbContext.DataBase.GetCollection<PickPointMachineEntity>(CollectionName);
    }
}