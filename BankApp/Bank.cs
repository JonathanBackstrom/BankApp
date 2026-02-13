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
    private List<AccountBase> Accounts { get; set; } = new List<AccountBase>();

    public void ShowBankMenu()
    {
        bool isRunning = true;
        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("=== BANK OF UDDEVALLA ===");
            Console.WriteLine("[1] Visa konton");
            Console.WriteLine("[2] Skapa konto");
            Console.WriteLine("[3] Ta bort konto");
            Console.WriteLine("[4] Hantera konto");
            Console.WriteLine("[5] Avsluta");

            MenuOption option = GetUserOption();

            switch (option)
            {
                case MenuOption.VisaKonton:
                    ShowAllAccounts();
                    break;
                case MenuOption.SkapaKonto:
                    CreateAccount();
                    break;
                case MenuOption.TaBortKonto:
                    RemoveAccount();
                    break;
                case MenuOption.HanteraKonton:
                    ManageAccount();
                    break;
                case MenuOption.Exit:
                    isRunning = false;
                    Console.WriteLine("Tack för att du använde Bank of Uddevalla!");
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, försök igen!");
                    Wait();
                    break;
            }
        }
    }

    private MenuOption GetUserOption()
    {
        var key = Console.ReadKey(intercept: true).KeyChar;
        return key switch
        {
            '1' => MenuOption.VisaKonton,
            '2' => MenuOption.SkapaKonto,
            '3' => MenuOption.TaBortKonto,
            '4' => MenuOption.HanteraKonton,
            '5' => MenuOption.Exit,
            _ => MenuOption.None
        };
    }

    private void ShowAllAccounts()
    {
        Console.Clear();
        Console.WriteLine("=== ALLA KONTON ===");

        if (!Accounts.Any())
        {
            Console.WriteLine("Inga konton hittades.");
        }

        foreach (var acc in Accounts)
        {
            Console.WriteLine($"Konto: {acc.AccountName} | Kontonummer: {acc.AccountNumber} | Saldo: {acc.Balance()} kr");
        }
        Wait();
    }

    private void CreateAccount()
    {
        Console.Clear();
        Console.WriteLine("Välj typ: [1] Bankkonto [2] ISK [3] UddevallaKonto");
        var choice = Console.ReadLine();

        Console.Write("Ange namn: ");
        string name = Console.ReadLine() ?? "Namnlöst";
        string nr = InputValidator.GetValidAccountNumber("Ange kontonummer:");


        if (choice == "1")
        {
            Accounts.Add(new BankAccount(name, nr));
        }
        else if (choice == "2")
        {
            Accounts.Add(new IskAccount(name, nr));
        }
        else if (choice == "3")
        {
            Accounts.Add(new UddevallaAccount(name, nr));
        }
        else
        {
            Console.WriteLine("Ogiltigt val, konto skapat som Bankkonto.");
            Accounts.Add(new BankAccount(name, nr));
        }
        Console.Clear();
        Console.WriteLine("Konto skapat!");
        Wait();
    }

    private void RemoveAccount()
    {
        if (Accounts.Count == 0)
        {
            Console.Clear();
            Console.WriteLine("Det finns inga konton att ta bort.");
            Wait();
            return;
        }

        Console.Clear();
        Console.WriteLine("Vilket konto vill du ta bort? (Ange nummer i listan)");

        for (int i = 0; i < Accounts.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {Accounts[i].AccountName} ({Accounts[i].AccountNumber})");
        }

        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Accounts.Count)
        {
            Accounts.RemoveAt(index - 1);
            Console.WriteLine("Kontot är borttaget!");
        }
        Wait();
    }
    private void ManageAccount()
    {
        Console.Clear();
        Console.WriteLine(" === VÄLJ KONTO ATT HANTERA ===");

        if (Accounts.Count == 0)
        {
            Console.WriteLine("Det finns inga konton att hantera.");
            Wait();
            return;
        }

        for (int i = 0; i < Accounts.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {Accounts[i].AccountName} ({Accounts[i].AccountNumber})");
        }

        Console.WriteLine("\nAnge siffra för kontot: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= Accounts.Count)
        {
            var selectedAccount = Accounts[index - 1];
            ShowSingleAccountMenu(selectedAccount);
        }
        else
        {
            Console.WriteLine("Ogiltigt val!");
            Wait();
        }
    }
    private void ShowSingleAccountMenu(AccountBase account)
    {
        bool isManaging = true;
        while (isManaging)
        {
            Console.Clear();
            Console.WriteLine($"=== HANTERAR KONTO: {account.AccountName} ({account.AccountNumber}) ===");
            Console.WriteLine($"Saldo: {account.Balance()} kr");
            Console.WriteLine("[1] Sätt in pengar");
            Console.WriteLine("[2] Ta ut pengar");
            Console.WriteLine("[3] Tillbaka till huvudmenyn");

            var choice = Console.ReadKey(intercept: true).KeyChar;
            switch (choice)
            {
                case '1':
                    Console.Clear();
                    decimal depositAmount = InputValidator.GetValidDecimal("Ange belopp att sätta in: ");
                    account.Deposit(depositAmount);
                    Console.WriteLine("Insättning genomförd!");
                    
                    Wait();
                    break;
                case '2':
                    Console.Clear();
                    Console.Write("Ange belopp att ta ut: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal withdrawAmount))
                    {
                        account.Withdraw(withdrawAmount);
                        Console.WriteLine("Pengar uttagna!");
                    }
                    Wait();
                    break;
                case '3':
                    isManaging = false;
                    break;
                default:
                    Console.WriteLine("Ogiltigt val!");
                    Wait();
                    break;
            }
        }
    }
    private void Wait()
    {
        Console.WriteLine("\nTryck på valfri tangent för att gå tillbaka...");
        Console.ReadKey();
    }
}

