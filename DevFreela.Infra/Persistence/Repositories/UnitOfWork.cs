using DevFreela.Domain.Interfaces;
using DevFreela.Infra.Persistence.Context;

namespace DevFreela.Infra.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DevFreelaDbContext _dbContext;

    public UnitOfWork(DevFreelaDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _dbContext.Database.CommitTransactionAsync(cancellationToken);
        }
        catch (Exception e)
        {
            await _dbContext.Database.RollbackTransactionAsync(cancellationToken);
            Console.WriteLine(e);
            throw;
        }
    }
}