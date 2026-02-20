using BankApp.Accounts;
using BankApp.Base;
using BankApp.Utils;

namespace BankApp;

internal class Program
{
    static void Main(string[] args)
    {
      //new Bank().ShowBankMenu();
       
        Bank myBank = new Bank();
        SeedData.Initialize(myBank);

        myBank.ShowBankMenu();

    }
}
