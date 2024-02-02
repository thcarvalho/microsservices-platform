namespace MSP.Data.Core;

public interface IUnitOfWork : IDisposable
{
    Task<bool> CommitAsync();
    Task RollbackAsync();
}