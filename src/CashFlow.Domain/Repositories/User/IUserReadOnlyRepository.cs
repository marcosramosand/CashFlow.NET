namespace CashFlow.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> ExistActiveUserWithEmail(string email);

    Task<Entidades.User> GetUserByEmail(string email);
}