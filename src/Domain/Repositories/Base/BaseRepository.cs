namespace Domain.Repositories.Base;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities);
    IQueryable<TEntity> Get();
    IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
    Task<TEntity> GetByIdAsync<TId>(TId id) where TId : notnull;
    Task UpdateAsync(TEntity entity);
    Task UpdateRange(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task DeleteRange(IEnumerable<TEntity> entities);
    void Dispose();
}