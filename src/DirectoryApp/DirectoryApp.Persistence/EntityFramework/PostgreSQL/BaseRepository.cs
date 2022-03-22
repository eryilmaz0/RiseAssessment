using System.Linq.Expressions;
using DirectoryApp.Application.Repository;
using DirectoryApp.Domain.Entity;
using DirectoryApp.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DirectoryApp.Persistence.EntityFramework.PostgreSQL;

public class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
{
    private readonly ApplicationContext _context;
    private readonly IQueryable<TEntity> _entity;

    public BaseRepository(ApplicationContext context)
    {
        _context = context;
        _entity = _context.Set<TEntity>();
    }


    public async Task<ICollection<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null, bool disableTracking = true)
    {
        if (disableTracking)
            return await _entity.AsNoTracking().ToListAsync();

        return await _entity.ToListAsync();
    }


    public async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null , bool disableTracking = true)
    {
        IQueryable<TEntity> query = _entity;

        if (disableTracking)
            query = query.AsNoTracking();

        if (include != null)
            query = include(query);

        return await query.FirstOrDefaultAsync(filter);
    }


    public async Task<bool> Insert(TEntity entity)
    {
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Remove(TEntity entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(TEntity entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}