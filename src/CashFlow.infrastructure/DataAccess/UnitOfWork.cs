using CashFlow.Domain.Repositories;

namespace CashFlow.infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{
    private readonly CashFlowdbContext _dbContext;
    public UnitOfWork(CashFlowdbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Comit() => await _dbContext.SaveChangesAsync();
}
