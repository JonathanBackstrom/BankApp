using BankApp.Accounts;
using BankApp.Factories;
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
            AccountMenu();

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

    private static void AccountMenu()
    {
        Console.Clear();
        Console.WriteLine("=== BANK OF UDDEVALLA ===");
        Console.WriteLine("[1] Visa konton");
        Console.WriteLine("[2] Skapa konto");
        Console.WriteLine("[3] Ta bort konto");
        Console.WriteLine("[4] Hantera konto");
        Console.WriteLine("[5] Räkna ut ränta (2025)");
        Console.WriteLine("[6] Avsluta");
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
            case MenuOption.RäknaRänta:
                CalculateInterest();
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
            '5' => MenuOption.RäknaRänta,
            '6' => MenuOption.Exit,
            _ => MenuOption.None
        };
    }
    private void ShowAllAccounts()
    {
        Console.Clear();
        Console.WriteLine("=== ALLA KONTON ===");
        if (!_bank.Accounts.Any())
        {
        Console.WriteLine("Inga konton hittades.");
        }

        foreach (var acc in _bank.Accounts)
        {
            Console.WriteLine($"Konto: {acc.AccountName} | Nr: {acc.AccountNumber} | Ränta: {acc.InterestRate:P1} | Saldo: {acc.Balance():C}");
        }
        Wait();
    }
    private void CreateAccount()
    {
        Console.Clear();
        Console.WriteLine("=== SKAPA NYTT KONTO ===");
        Console.WriteLine("Välj typ: [1] Bankkonto [2] ISK [3] Uddevalla-Konto [4] Coop-Konto [5] Ica-Konto");
        var choiceInput = Console.ReadLine();

        //Validera att input är en giltig enum och definierad i AccountType
        if (!Enum.TryParse(choiceInput, out AccountType selectedType) || !Enum.IsDefined(typeof(AccountType), selectedType))
        {
            Console.WriteLine("Ogiltigt val Du måste välja mellan 1 - 5.");
            Wait();
            return;
        }

        Console.Clear();
        //AccountName
        Console.Write("Ange namn för ditt konto: ");
        string accountName = Console.ReadLine() ?? "Namnlöst";

        //AccountNumber
        string accountNumber = InputValidator.GetValidAccountNumber("Ange kontonummer: ");

        //Skapa AccountDetails och sedan konto via fabriken
        var accountDetails = new AccountDetails
        {
            AccountName = accountName,
            AccountNumber = accountNumber,
            AccountType = selectedType
        };

        //Skapa konto via fabriken
        var newAccount = AccountFactory.CreateAccount(accountDetails);

        //Lägg till det nya kontot i banken
        _bank.AddAccount(newAccount);

        Console.Clear();
        Console.WriteLine($"Konto har skapats framgångsrikt! - {newAccount.AccountName}");
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
            Console.WriteLine($"Saldo: {account.Balance():C} | Ränta: {account.InterestRate:P1} ");
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
    
    private void CalculateInterest()
    {
        Console.Clear();
        Console.WriteLine("=== RÄNTEUTBETALNING (DAG-FÖR-DAG BERÄKNING) ===");

        _bank.ProcessYearlyInterest(2025);

        Console.WriteLine("\nBeräkningen är slutförd baserat på 365 dagar.");
        Wait();
    }
    private void Wait()
    {
        Console.WriteLine("\nTryck på valfri tangent för att gå tillbaka...");
        Console.ReadKey();
    }
}