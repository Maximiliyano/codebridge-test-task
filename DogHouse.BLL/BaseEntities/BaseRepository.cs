using System.Linq.Expressions;
using DogHouse.BLL.Interfaces.Repositories;
using DogHouse.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DogHouse.BLL.BaseEntities;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly DogHouseDbContext _context;

    protected BaseRepository(DogHouseDbContext context)
    {
        _context = context;
    }

    public virtual async Task CreateAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        await SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.Set<TEntity>().Update(entity);
        await _context.SaveChangesAsync();
        _context.Entry(entity).State = EntityState.Detached;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression)
    {
        var response = await _context.Set<TEntity>().Where(expression).AsQueryable().FirstAsync();
        _context.Entry(response).State = EntityState.Detached;

        return response;
    }

    public virtual async Task<TEntity?> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression)
    {
        var response = await _context.Set<TEntity>().Where(expression).AsQueryable().FirstOrDefaultAsync();
        if (response is not null)
            _context.Entry(response).State = EntityState.Detached;

        return response;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var response = await _context.Set<TEntity>().AsNoTracking().ToListAsync();

        return response;
    }
    
    public virtual async Task<TEntity?> GetByIdAsync(int id)
    {
        var response = await _context.Set<TEntity>().FindAsync(id);
        return response;
    }
}