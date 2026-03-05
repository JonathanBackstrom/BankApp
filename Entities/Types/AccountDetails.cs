using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Types;

public class AccountDetails
{
    public string AccountName { get; set; } = "";
    public string AccountNumber { get; set; } = "";
    public AccountType AccountType { get; set; }
}
