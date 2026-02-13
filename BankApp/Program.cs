using BankApp.Accounts;
using BankApp.Base;

namespace BankApp;

internal class Program
{
    static void Main(string[] args)
    {
      new Bank().ShowBankMenu();
    }
}
