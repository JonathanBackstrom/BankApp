using BankApp.Accounts;
using BankApp.Base;

namespace BankApp;

internal class Program
{
    static void Main(string[] args)
    {
        var b = new Bank();
        var listOfAccounts = b.GetAccounts();


    }
}
