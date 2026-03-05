using Entities.Accounts;
using Entities.Base;
using BankApp.Utils;

namespace BankApp;

public class Program
{
    static void Main(string[] args)
    {
      //new Bank().ShowBankMenu();
       
        Bank myBank = new Bank();
        SeedData.Initialize(myBank);

        myBank.ShowBankMenu();

    }
}
