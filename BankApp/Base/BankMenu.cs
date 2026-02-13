using BankApp.Accounts;
using BankApp.Models;
using BankApp.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Text;

namespace BankApp.Base;

internal class BankMenu
{
    private readonly Bank _bank;

    public BankMenu(Bank bank)
    {
        _bank = bank;
    }

    public void Run()
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

            if (option == MenuOption.Exit)
            {
                isRunning = false;
                Console.WriteLine("Tack för att du använde Bank of Uddevalla!");
                Wait();
            }
            else
            {
                HandleOption(option);
            }
        }
    }

    private void HandleOption(MenuOption option)
    {
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
            default:
                Console.WriteLine("Ogiltigt val, försök igen!");
                Wait();
                break;
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
        if (!_bank.Accounts.Any()) Console.WriteLine("Inga konton hittades.");

        foreach (var acc in _bank.Accounts)
            Console.WriteLine($"Konto: {acc.AccountName} | Nr: {acc.AccountNumber} | Saldo: {acc.Balance():C}");
        Wait();
    }
    private void CreateAccount()
    {
        Console.Clear();
        Console.WriteLine("=== SKAPA NYTT KONTO ===");
        Console.WriteLine("Välj typ: [1] Bankkonto [2] ISK [3] UddevallaKonto");
        var choice = Console.ReadLine();

        Console.Clear();
        Console.Write("Ange namn för ditt konto: ");
        string name = Console.ReadLine() ?? "Namnlöst";

        string nr = InputValidator.GetValidAccountNumber("Ange kontonummer: ");

        if (choice == "1")
        {
            _bank.AddAccount(new BankAccount(name, nr));
        }
        else if (choice == "2")
        {
            _bank.AddAccount(new IskAccount(name, nr));
        }
        else if (choice == "3")
        {
            _bank.AddAccount(new UddevallaAccount(name, nr));
        }
        else
        {
            Console.WriteLine("\nOgiltigt val av kontotyp! Skapar ett standard Bankkonto.");
            _bank.AddAccount(new BankAccount(name, nr));
        }

        Console.Clear();
        Console.WriteLine("\nKonto har skapats framgångsrikt!");
        Wait();
    }
    private void RemoveAccount()
    {
        Console.Clear();
        if (_bank.Accounts.Count == 0)
        { 
            Console.WriteLine("Det finns nuvarande inga konton.");
            Wait();
            return;
        }

        for (int i = 0; i < _bank.Accounts.Count; i++)
            Console.WriteLine($"{i + 1}. {_bank.Accounts[i].AccountName}");

        if (int.TryParse(Console.ReadLine(), out int idx) && _bank.RemoveAccountAt(idx - 1))
        { 
            Console.WriteLine("Borttaget!");
        }
        else
        {
            Console.WriteLine("Ogiltigt val. Inget konto har tagits bort!");
        }
        Wait();
    }
    private void ManageAccount()
    {
        Console.Clear();
        Console.WriteLine(" === VÄLJ KONTO ATT HANTERA ===");

        if (_bank.Accounts.Count == 0)
        {
            Console.WriteLine("Det finns inga konton att hantera.");
            Wait();
            return;
        }

        for (int i = 0; i < _bank.Accounts.Count; i++)
        {
            Console.WriteLine($"[{i + 1}] {_bank.Accounts[i].AccountName} ({_bank.Accounts[i].AccountNumber})");
        }

        Console.WriteLine("\nAnge siffra för kontot: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= _bank.Accounts.Count)
        {
            var selectedAccount = _bank.Accounts[index - 1];
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

                    if (account.Deposit(depositAmount))
                    {
                        Console.WriteLine("Insättning genomförd!");
                    }

                    Wait();
                    break;

                case '2':
                    Console.Clear();
                    decimal withdrawAmount = InputValidator.GetValidDecimal("Ange belopp att ta ut: ");

                    if (account.Withdraw(withdrawAmount))
                    {
                        Console.WriteLine("Uttag genomfört!");
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