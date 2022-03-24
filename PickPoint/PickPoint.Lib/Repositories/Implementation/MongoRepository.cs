using System.Linq.Expressions;
using MongoDB.Driver;
using PickPoint.Lib.Contexts;
using PickPoint.Lib.Domain.Common;
using PickPoint.Lib.Repositories.Declaration;

namespace PickPoint.Lib.Repositories.Implementation;

    public abstract class MongoRepository<T> : IMongoRepository<T> where T : IEntity
    {
        protected readonly MongoContext DbContext;
        protected IMongoCollection<T> Entities;

        protected MongoRepository(MongoContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
        }
        
        public virtual async Task CreateAsync(T entity, CancellationToken token = default) => 
            await Entities.InsertOneAsync(entity, cancellationToken: token);

        public virtual async Task<T> ReadAsync(string id, CancellationToken token = default) =>
            await Entities.Find(item => item.Id == id)
                .FirstOrDefaultAsync(token);

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> filter, CancellationToken token = default)=>
            await Entities.Find(filter).FirstOrDefaultAsync(token);

        public virtual async Task<IQueryable<T>> AllAsync(CancellationToken token = default) => 
            await Task.Run(() => Entities.AsQueryable(), token);

        public virtual async Task<IQueryable<T>> FilterAsync(Func<T, bool> filter, CancellationToken token)
        {
            return await Task.Run(() => Entities.AsQueryable().Where(filter).AsQueryable(), token);
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken token = default)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));

            await Entities.FindOneAndReplaceAsync(item => item.Id == entity.Id, entity, cancellationToken: token);
        }

        public virtual async Task<bool> DeleteAsync(string id, CancellationToken token = default)
        {
            var result = await Entities.DeleteOneAsync(item => item.Id == id, token);

            return result.IsAcknowledged;
        }
    }