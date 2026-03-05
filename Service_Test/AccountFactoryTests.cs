using Entities.Accounts;
using Entities.Types;
using Services.Factories;

namespace Services_Test;

public class AccountFactoryTests
{
    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnCoopAccount()
    {
        //Arrange

        var accountDetails = new AccountDetails
        {
            AccountName = "Testing",
            AccountNumber = "11",
            AccountType = AccountType.CoopAccount
        };


        //Act

       var account = AccountFactory.CreateAccount(accountDetails);

        //assert

        Assert.IsType<CoopAccount>(account);
    }
    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnIcaAccount()
    {
        //Arrange

        var accountDetails = new AccountDetails
        {
            AccountName = "Testing",
            AccountNumber = "11",
            AccountType = AccountType.IcaAccount
        };


        //Act

       var account = AccountFactory.CreateAccount(accountDetails);

        //assert

        Assert.IsType<IcaAccount>(account);
    }
    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnIskAccount()
    {
        //Arrange

        var accountDetails = new AccountDetails
        {
            AccountName = "Testing",
            AccountNumber = "11",
            AccountType = AccountType.IskAccount
        };


        //Act

       var account = AccountFactory.CreateAccount(accountDetails);

        //assert

        Assert.IsType<IskAccount>(account);
    }
    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnUddevallaAccount()
    {
        //Arrange

        var accountDetails = new AccountDetails
        {
            AccountName = "Testing",
            AccountNumber = "11",
            AccountType = AccountType.UddevallaAccount
        };


        //Act

       var account = AccountFactory.CreateAccount(accountDetails);

        //assert

        Assert.IsType<UddevallaAccount>(account);
    }
    [Fact]
    public void AccountFactory_CreateAccount_ShouldReturnBankAccount()
    {
        //Arrange

        var accountDetails = new AccountDetails
        {
            AccountName = "Testing",
            AccountNumber = "11",
            AccountType = AccountType.BankAccount
        };


        //Act

       var account = AccountFactory.CreateAccount(accountDetails);

        //assert

        Assert.IsType<BankAccount>(account);
    }
}
