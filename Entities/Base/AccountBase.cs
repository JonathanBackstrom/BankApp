using System;
using System.Collections.Generic;
using System.Text;
using Entities.Types;

namespace Entities.Base;

public abstract class AccountBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
    protected decimal StartingBalance { get; set; } = 0;
    public string AccountName { get; set; } = "";
    public string AccountNumber { get; set; }= "";

    public decimal InterestRate { get; set; }

    protected List<BankTransaction> BankTransactions = new();

    protected AccountBase(string accountName, string accountNumber)
    {
        AccountName = accountName;
        AccountNumber = accountNumber;
    }

    public abstract decimal Balance();

    public virtual bool Deposit(decimal amount, DateTime? date = null)
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

    public virtual bool Withdraw(decimal amount)
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
        DateTime startDate = new(year, 1, 1);
        DateTime endDate = new (year, 12, 31);

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
