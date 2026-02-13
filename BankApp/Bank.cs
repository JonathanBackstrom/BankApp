using BankApp.Base;
using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Accounts;
using BankApp.Models;
using BankApp.Utils;

namespace BankApp;

internal class Bank
{
    public List<AccountBase> Accounts { get; set; } = new List<AccountBase>();

    public void ShowBankMenu()
    {
        var menu = new BankMenu(this);
        menu.Run();
    }

    public void AddAccount(AccountBase account)
    {
        Accounts.Add(account);
    }
    public bool RemoveAccountAt(int index)
    {
        if (index >= 0 && index < Accounts.Count)
        {
            Accounts.RemoveAt(index);
            return true;
        }
        return false;
    }
}

