using CashFlow.Domain.Entidades;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CashFlow.Infrastructure.DataAccess;

internal class CashFlowdbContext : DbContext
{
    public CashFlowdbContext(DbContextOptions options) : base (options){ }
    public DbSet<Expense> Expenses { get; set; }

    public DbSet<User> Users { get; set; }

}
