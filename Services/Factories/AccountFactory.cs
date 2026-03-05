using Entities.Accounts;
using Entities.Base;
using Entities.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Factories;

public static class AccountFactory
{
    public static AccountBase CreateAccount(AccountDetails accountDetails)
    {
        return accountDetails.AccountType switch
        {
            AccountType.BankAccount => new BankAccount(accountDetails.AccountName, accountDetails.AccountNumber),
            AccountType.IskAccount => new IskAccount(accountDetails.AccountName, accountDetails.AccountNumber),
            AccountType.UddevallaAccount => new UddevallaAccount(accountDetails.AccountName, accountDetails.AccountNumber),
            AccountType.CoopAccount => new CoopAccount(accountDetails.AccountName, accountDetails.AccountNumber),
            AccountType.IcaAccount => new IcaAccount(accountDetails.AccountName, accountDetails.AccountNumber),
            _ => throw new NotImplementedException(),
        };
    }
}
