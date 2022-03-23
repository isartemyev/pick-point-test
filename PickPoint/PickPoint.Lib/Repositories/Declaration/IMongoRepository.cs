using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PickPoint.Lib.Domain.Common;

namespace PickPoint.Lib.Repositories.Declaration;

public interface IMongoRepository<TEntity> where TEntity : IEntity
{
    Task<bool> CreateAsync(TEntity entity, CancellationToken ct = default);

    Task<TEntity> ReadAsync(string id, CancellationToken ct = default);

    Task<IQueryable<TEntity>> AllAsync(CancellationToken ct = default);

    Task<IQueryable<TEntity>> FilterAsync(Func<TEntity, bool> filter, CancellationToken ct = default);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken ct = default);

    Task<bool> UpdateManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default);

    Task<bool> DeleteAsync(string id, CancellationToken ct = default);

    Task<bool> DeleteAsync(TEntity entity, CancellationToken ct = default);

    Task<bool> DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken ct = default);
}