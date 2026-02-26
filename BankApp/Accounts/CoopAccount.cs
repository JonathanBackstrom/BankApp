using BankApp.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Accounts;

internal class CoopAccount : AccountBase
{
    public CoopAccount(string name, string number) : base(name, number)
    {
        InterestRate = 0.02m;
    }

    internal override decimal Balance()
    {
        return BankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
