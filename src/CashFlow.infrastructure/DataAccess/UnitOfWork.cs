using CashFlow.Domain.Repositories;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowdbContext _dbContext;
    public UnitOfWork(CashFlowdbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
