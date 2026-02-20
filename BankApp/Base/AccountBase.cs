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

    public decimal InterestRate { get; protected set; }

    protected List<BankTransaction> BankTransactions = new List<BankTransaction>();

    protected AccountBase(string accountName, string accountNumber)
    {
        AccountName = accountName;
        AccountNumber = accountNumber;
    }

    internal abstract decimal Balance();

    internal virtual bool Deposit(decimal amount, DateTime? date = null)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Beloppet måste vara större än noll.");
            return false; 
        }

        var t = new BankTransaction
        {
            Amount = amount,
            TransactionalDate = date ?? DateTime.Now
        };
        BankTransactions.Add(t);
        return true;
    }

    internal virtual bool Withdraw(decimal amount)
    {
        var balance = Balance();
        if (amount <= 0)
        {
            Console.WriteLine("Beloppet måste vara större än noll.");
            return false;
        }

        if (balance < amount)
        {
            Console.WriteLine("Otillräckligt saldo"); 
            return false;
        }

        var t = new BankTransaction
        {
            Amount = -amount,
            TransactionalDate = DateTime.Now
        };

        BankTransactions.Add(t);
        return true;
    }

    public decimal CalculateYearlyInterest(int year)
    {
        decimal totalInterest = 0;
        DateTime startDate = new DateTime(year, 1, 1);
        DateTime endDate = new DateTime(year, 12, 31);

        for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
        {
            decimal balanceAtDate = BankTransactions
                .Where(t => t.TransactionalDate.Date <= date.Date)
                .Sum(t => t.Amount);

            totalInterest += (balanceAtDate * InterestRate) / 365;
        }

        return totalInterest;
    }
}
