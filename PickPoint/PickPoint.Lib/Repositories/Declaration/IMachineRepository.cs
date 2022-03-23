using PickPoint.Lib.Domain.Core.Machine;

namespace PickPoint.Lib.Repositories.Declaration;

public interface IMachineRepository : IMongoRepository<PickPointMachineEntity>
{
    
}