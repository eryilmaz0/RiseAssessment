using System.Linq.Expressions;
using DirectoryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace DirectoryApp.Application.Repository;

public interface IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{
    public Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, bool disableTracking = true);
    public Task<TEntity> Find(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = true);
    public Task<bool> Insert(TEntity entity);
    public Task<bool> Remove(TEntity entity);
    public Task<bool> Update(TEntity entity);
}