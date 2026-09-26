using CashFlow.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Entidades;

public  class User
{
    public long Id { get; set; }
    public String Name { get; set; } = string.Empty;

    public string Email { get; set; }  = string.Empty;

    public string Password  { get; set; } = string.Empty;

    public Guid UserIdentifier { get; set; }

    public string Role { get; set; } = Roles.TEAM_MEMBER;

}
