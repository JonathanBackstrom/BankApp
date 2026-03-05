using Entities.Accounts;

namespace Entities_Test;

public class AccountBaseTests
{
    [Fact]
    public void AccountBase_CalculateYearlyInterestBankaccount()
    {
        //Arrange
        var account = new BankAccount("test", "123");

        account.Deposit(1000, new DateTime(2025, 1, 1));
        //Act
        decimal interest = account.CalculateYearlyInterest(2025);

        //Asset
        Assert.Equal(10.00m, interest, 2); //2 står för antal decimaler
    }

    [Theory]
    [InlineData(1000, 0.045, 45.00)]
    [InlineData(10000, 0.01, 100.00)]
    [InlineData(0, 0.05, 00.00)]
    public void AccountBase_CalculateYearlyInterestBankaccount_WithDiffrentStartBalanceses_ReturnCorrectAmount(decimal startBalance, decimal rate, decimal expectedInterest)
    {
        //Arrange
        var account = new UddevallaAccount("Test", "123");
        account.InterestRate = rate;

        account.Deposit(startBalance, new DateTime(2025, 1, 1));
        //Act
        decimal interest = account.CalculateYearlyInterest(2025);
        //Asset
        Assert.Equal(expectedInterest, interest, 2);
    }
}
