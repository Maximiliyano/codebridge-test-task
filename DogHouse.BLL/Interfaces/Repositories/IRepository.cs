using System.Linq.Expressions;

namespace DogHouse.BLL.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task CreateAsync(TEntity entity);
    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> expression);
    Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
    Task UpdateAsync(TEntity entity);
}