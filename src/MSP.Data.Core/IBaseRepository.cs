using System.Linq.Expressions;
using MSP.Core.Models;

namespace MSP.Data.Core;

public interface IBaseRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity
{
    TEntity Create(TEntity entity);
    Task<TEntity> CreateAsync(TEntity entity);
    IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities);
    Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
    TEntity Update(TEntity entity);
    IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities);
    TEntity DeleteOne(TEntity entity);
    Task<TEntity> DeleteByIdAsync(int id);
    IEnumerable<TEntity> DeleteMany(IEnumerable<TEntity> entities);
    Task<IEnumerable<TEntity>> DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter);
}