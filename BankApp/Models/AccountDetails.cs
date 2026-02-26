using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models;

internal class AccountDetails
{
    internal string AccountName { get; set; } = "";
    internal string AccountNumber { get; set; } = "";
    internal AccountType AccountType { get; set; }
}
