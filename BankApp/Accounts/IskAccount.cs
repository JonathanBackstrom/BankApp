using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Base;

namespace BankApp.Accounts;

internal class IskAccount : AccountBase
{
    internal override decimal Balance()
    {
        var t = BankTransactions.Sum(x => x.Amount);
        return t + StartingBalance;
    }
}
