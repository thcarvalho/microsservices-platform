using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MSP.Core.Extensions;
using MSP.Core.Models;
using MSP.Core.Params;

namespace MSP.Data.Core;

public class ReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public ReadOnlyRepository(DbContext context)
    {
        _dbSet = context.Set<TEntity>();
        _context = context;
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return _dbSet.AsQueryable();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>> filter = null,
        IPaginable pagination = null,
        string orderBy = null,
        string includeProps = null,
        bool asNoTracking = true)
    {
        return await GetQueryable(filter, pagination, orderBy, includeProps, asNoTracking).ToListAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAsync(
        IParams<TEntity> @params, 
        string? includeProps = null,
        bool? asNoTracking = true)
    {
        return await GetQueryable(@params, includeProps, asNoTracking).ToListAsync();
    }

    public async Task<TEntity> GetOneAsync(
        Expression<Func<TEntity, bool>> filter = null, 
        string includeProps = null,
        bool asNoTracking = true)
    {
        return await GetQueryable(filter, includeProps: includeProps, asNoTracking: asNoTracking).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(
        string? includeProps = null, 
        bool asNoTracking = true)
    {
        return await GetQueryable(includeProps: includeProps, asNoTracking: asNoTracking).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id, string includeProps = null)
    {
        return await GetQueryable(includeProps: includeProps).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await GetQueryable(filter).AnyAsync();
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await GetQueryable(x => x.Id == id).AnyAsync();
    }

    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
    {
        return await GetQueryable(filter).CountAsync();
    }

    protected virtual IQueryable<TEntity> GetQueryable(IParams<TEntity> @params, string? includeProps = null,
        bool? asNoTracking = true)
    {
        var filter = @params as IFiltrable<TEntity>;
        var pagination = @params as IPaginable;
        var sorting = @params as ISortable;
        return GetQueryable(filter?.Filter(), pagination, sorting.OrderBy, includeProps, asNoTracking);
    }

    protected virtual IQueryable<TEntity> GetQueryable(
        Expression<Func<TEntity, bool>> filter = null,
        IPaginable pagination = null,
        string? orderBy = null,
        string? includeProps = null,
        bool? asNoTracking = true)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(includeProps))
            query = includeProps.Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Aggregate(query, (q, p) => q.Include(p));

        if (filter is not null)
            query = query.Where(filter);

        if (!string.IsNullOrEmpty(orderBy))
            query = query.OrderBy(orderBy);

        if (pagination is not null)
        {
            if (pagination.Skip.HasValue)
                query = query.Skip(pagination.Skip.Value);

            if (pagination.Take.HasValue)
                query = query.Take(pagination.Take.Value);
        }

        if (asNoTracking.Value)
            query = query.AsNoTracking();

        return query;
    }
}