using DevFreela.Persistence.Context;

namespace DevFreela.Persistence.Repositories;

public class UnitOfWork
{
    private readonly DevFreelaDbContext _dbContext;

    public UnitOfWork(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginTransactionAsync()
    {
        await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _dbContext.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _dbContext.Database.RollbackTransactionAsync();
    }
}