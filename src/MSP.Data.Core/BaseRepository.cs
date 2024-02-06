using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MSP.Core.Models;

namespace MSP.Data.Core;

public class BaseRepository<TEntity> : ReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : Entity
{
    public BaseRepository(DbContext context) : base(context)
    {
    }

    public TEntity Create(TEntity entity)
    {
        var entityEntry = _dbSet.Add(entity);
        return entityEntry.Entity;
    }

    public TEntity Update(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);
        return entityEntry.Entity;
    }

    public IEnumerable<TEntity> CreateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
        return entities;
    }

    public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
        return entities;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        var entityEntry = await _dbSet.AddAsync(entity);
        return entityEntry.Entity;
    }

    public async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        return entities;
    }

    public TEntity DeleteOne(TEntity entity)
    {
        _dbSet.Remove(entity);
        return entity;
    }

    public async Task<TEntity> DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return DeleteOne(entity);
    }

    public IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            entity.SetActive(false);
        return UpdateRange(entities);
    }

    public async Task<IEnumerable<TEntity>> DeleteByFilterAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entities = await GetAsync(filter);
        foreach (var entity in entities)
            entity.SetActive(false);
        return UpdateRange(entities);
    }

    public TEntity DisableOne(TEntity entity)
    {
        entity.SetActive(false);
        return Update(entity);
    }

    public async Task<TEntity> DisableByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        return DisableOne(entity);
    }

    public IEnumerable<TEntity> DisableRange(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            entity.SetActive(false);
        return UpdateRange(entities);
    }

    public async Task<IEnumerable<TEntity>> DisableByFilterAsync(Expression<Func<TEntity, bool>> filter)
    {
        var entities = await GetAsync(filter);
        foreach (var entity in entities)
            entity.SetActive(false);
        return UpdateRange(entities);
    }
}