using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Base;

namespace BankApp.Accounts;

internal class UddevallaAccount : AccountBase
{
    public UddevallaAccount(string name, string number) : base(name, number)
    {
        InterestRate = 0.045m;
    }

    internal override decimal Balance()
    {
        return BankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
