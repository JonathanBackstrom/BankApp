using Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using Entities.Accounts;
using Entities.Types;
using BankApp.Utils;
using BankApp.Menu;

namespace BankApp;

public class Bank
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

    public void ProcessYearlyInterest(int year)
    {
        Console.WriteLine($"{"KONTOTYP",-25} | {"RÄNTA",8} | {"SALDO",12} | {"RÄNTEVINST",12}");
        Console.WriteLine(new string('-', 60));

        decimal totalBankInterest = 0;

        foreach (var account in Accounts)
        {
            decimal interest = account.CalculateYearlyInterest(year);
            totalBankInterest += interest;

            
            Console.WriteLine($"{account.AccountName,-20} | " + $"{account.InterestRate,8:P1} | " + $"{account.Balance(),12:C} | " + $"{interest,12:C}");
        }

        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"Total beräknad ränta att utbetala: {totalBankInterest:C}");
    }

}

