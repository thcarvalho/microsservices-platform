using Microsoft.EntityFrameworkCore;
using MSP.Core.Models;

namespace MSP.Data.Core;

public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;

    public UnitOfWork(TContext context)
    {
        _context = context;
    }

    public async Task<bool> CommitAsync()
    {
        foreach (var entry in _context.ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Modified)
                entry.Entity.UpdatedAtNow();
        }
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task RollbackAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
        => _context.Dispose();
}