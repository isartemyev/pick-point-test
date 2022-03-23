namespace PickPoint.Lib.Repositories.Declaration;

public interface IMongoRepository<T>
{
    Task CreateAsync(T entity, CancellationToken token = default);

    Task<T> ReadAsync(string id, CancellationToken token = default);

    Task<IQueryable<T>> AllAsync(CancellationToken token = default);

    Task<IQueryable<T>> FilterAsync(Func<T, bool> filter, CancellationToken token = default);

    Task UpdateAsync(T entity, CancellationToken token = default);

    Task UpsertAsync(T entity, CancellationToken token = default);

    Task<bool> DeleteAsync(string id, CancellationToken token = default);
}