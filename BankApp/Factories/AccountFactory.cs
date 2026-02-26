using BankApp.Accounts;
using BankApp.Base;
using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Factories;

internal static class AccountFactory
{
    internal static AccountBase CreateAccount(AccountDetails accountDetails)
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
