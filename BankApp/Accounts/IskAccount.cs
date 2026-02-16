using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Base;

namespace BankApp.Accounts;

internal class IskAccount : AccountBase
{
    public IskAccount(string name, string number) : base(name, number)
    {
        InterestRate = 0.00m;
    }

    internal override decimal Balance()
    {
        return BankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
