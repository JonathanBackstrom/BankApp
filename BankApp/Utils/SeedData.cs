using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;
using BankApp.Accounts;

namespace BankApp.Utils;

internal class SeedData
{
    //Testdata för att visa att ränta fungerar, och att det går att göra insättningar på kontot.
    public static void Initialize(Bank bank)
    {
        var testAccount = new UddevallaAccount("Testkonto UddevallaKonto", "1234-456");

        decimal amount = 1000;

        for (int i = 1; i <= 12; i++)
        {
            testAccount.Deposit(amount, new DateTime(2025, i, 1));
        }

        bank.AddAccount(testAccount);
    }
}
