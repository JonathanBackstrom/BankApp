using System;
using System.Collections.Generic;
using System.Text;
using BankApp.Models;

namespace BankApp.Base;

internal abstract class AccountBase
{
    internal Guid Id { get; set; } = Guid.NewGuid();
    protected decimal StartingBalance { get; set; } = 0;
    internal string AccountName { get; set; } = "";
    internal string AccountNumber { get; set; }= "";

    internal decimal IntrestRate { get; set; } = 0;

    protected List<BankTransaction> BankTransactions = new List<BankTransaction>();

    protected AccountBase(string accountName, string accountNumber)
    {
        AccountName = accountName;
        AccountNumber = accountNumber;
    }

    internal abstract decimal Balance();
    
    internal virtual void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
           Console.WriteLine("Amount must be greater than zero.");
            return;
        }

        var t = new BankTransaction
        {
            Amount = amount,
            TransactionalDate = DateTime.Now
        };
        BankTransactions.Add(t);
    }
    internal virtual void Withdraw(decimal amount)
    {
        var balance = Balance();
        if (amount <= 0)
        {
            Console.WriteLine("Amount must be greater than zero.");
            return;
        }

        if (balance < amount)
        {
            Console.WriteLine("Insufficent funds");
            return;
        }

        var t = new BankTransaction
        {
            Amount = -amount,
            TransactionalDate = DateTime.Now
        };

        BankTransactions.Add(t);
    }
}
