using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Domain.Repositories.User;

internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly CashFlowdbContext _dbContext;

    public UserRepository(CashFlowdbContext dbContext) => _dbContext = dbContext;

    public async Task Add(Entidades.User user)
    {
       await _dbContext.Users.AddAsync(user);

    }

    public async  Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email.Equals(email));
    }

    public async Task<Entidades.User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(email));
    }   
}
