using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Accounts;

public class CoopAccount : AccountBase
{
    public CoopAccount(string name, string number) : base(name, number)
    {
        InterestRate = 0.02m;
    }

    public override decimal Balance()
    {
        return BankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
