using System.Linq.Expressions;
using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;
using Microsoft.EntityFrameworkCore.Query;

namespace DirectoryApp.Persistence.EntityFramework.PostgreSQL;

public class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{
    public async Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, bool disableTracking = true)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter, IIncludableQueryable<TEntity, bool> include = null, bool disableTracking = true)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Insert(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Remove(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
}