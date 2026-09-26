namespace CashFlow.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    Task Add(Entidades.User user);
}
