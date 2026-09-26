using CashFlow.infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.infrastructure.Migrations;

public static class DataBaseMigration
{
    public static async Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        
        var dbcontext = serviceProvider.GetRequiredService<CashFlowdbContext>();
        await dbcontext.Database.MigrateAsync();
    }


}
