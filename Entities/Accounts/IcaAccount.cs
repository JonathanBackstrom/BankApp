using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Accounts;

public class IcaAccount : AccountBase
{
    public IcaAccount(string name, string number) : base(name, number)
    {
        InterestRate = 0.03m;
    }

    public override decimal Balance()
    {
        return BankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
