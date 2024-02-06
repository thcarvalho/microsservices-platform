using System.Linq.Expressions;
using MSP.Core.Models;
using MSP.Core.Params;

namespace MSP.Data.Core;

public interface IReadOnlyRepository<TEntity> where TEntity : Entity
{
    IQueryable<TEntity> AsQueryable();

    Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        IPaginable pagination = null,
        string orderBy = null,
        string includeProps = null,
        bool asNoTracking = true);

    Task<IEnumerable<TEntity>> GetAsync(
        IParams<TEntity> @params,
        string? includeProps = null,
        bool? asNoTracking = true);

    Task<TEntity?> GetOneAsync(
        Expression<Func<TEntity, bool>> filter = null,
        string includeProps = null,
        bool asNoTracking = true);

    Task<IEnumerable<TEntity>> GetAllAsync(
        string? includeProps = null,
        bool asNoTracking = true);

    Task<TEntity?> GetByIdAsync(int id, string includeProps = null);

    Task<bool> ExistsAsync(
        Expression<Func<TEntity, bool>> filter = null);

    Task<bool> ExistsByIdAsync(int id);

    Task<int> CountAsync(
        Expression<Func<TEntity, bool>> filter = null);
}