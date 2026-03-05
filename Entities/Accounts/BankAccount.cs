using System;
using System.Collections.Generic;
using System.Text;
using Entities.Base;
namespace Entities.Accounts;

public class BankAccount : AccountBase
{
    public BankAccount(string name, string number) : base(name, number)
    {
        InterestRate = 0.01m;
    }

    public override decimal Balance()
    {
        return BankTransactions.Sum(x => x.Amount) + StartingBalance;
    }
}
