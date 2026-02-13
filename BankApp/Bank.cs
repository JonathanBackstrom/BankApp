using BankApp.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp;

internal class Bank
{
    private List<AccountBase> Accounts { get; set; } = new List<AccountBase>();

    internal void AddAccount(AccountBase account)
    {
        Accounts.Add(account);
    }

    internal void RemoveAccount(Guid accountId)
    {
        var account = Accounts.FirstOrDefault(x => x.Id == accountId);
        if (account != null)
        {
            Accounts.Remove(account);
        }

    }
    internal List<AccountBase> GetAccounts()
    {
        return Accounts;
    }
}
