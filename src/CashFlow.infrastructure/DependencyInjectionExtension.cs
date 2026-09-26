using CashFlow.Application.UsesCases.Login.DoLogin;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.User;
using CashFlow.Domain.Security;
using CashFlow.Domain.Security.Tokens;
using CashFlow.infrastructure.DataAccess.Repositories;
using CashFlow.infrastructure.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.infrastructure;

public static class DependencyInjectionExtension
{
 
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddDbContext(services, configuration);
        AddTokenGenerator(services, configuration);

        services.AddScoped<IPassrowordEncripter , Security.cryptography.BCrypt>();
        services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();



    }

    private static void AddTokenGenerator(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutes = configuration.GetValue<uint>("Setting:Jwt:ExpirationTimeMinutes");
        var signingKey = configuration.GetValue<string>("Setting:Jwt:SigningKey");
        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutes, signingKey!));
    }
    private static void  AddRepositories(IServiceCollection services)
    {

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesWriteOnlyRepository, ExpensesRepository>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepository>();
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        
    }
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var conectionString = configuration.GetConnectionString("Connection");

        var version = new Version(8, 0, 46);
        var serverVersion = new MySqlServerVersion(version);



        services.AddDbContext<CashFlowdbContext>(config => config.UseMySql(conectionString, serverVersion));

    }
}
