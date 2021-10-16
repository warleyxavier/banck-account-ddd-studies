using bank_account_ddd_studies.domain.entity;
using bank_account_ddd_studies.domain.exception;
using FluentAssertions;
using Xunit;

namespace Tests.domain.entity
{
    public class AccountTests
    {
        [Fact]
        public void Credit_GivenAccountWithZeroBalance_ShouldCreditAsExpected()
        {
            const decimal amountToCredit = 1200;

            var account = new Account();

            account.Credit(amountToCredit);

            ValidateCurrentBalanceAccount(account, amountToCredit);
        }

        [Theory]
        [InlineData(10.50, 10.51)]
        [InlineData(0, 10.51)]
        [InlineData(10, 20)]
        public void Debit_GivenAccountWithBalanceLowerThatDebitAmount_ShouldThrowEInsufficientBalanceToOperationException(decimal balanceAmount, decimal debitAmount)
        {
            var account = new Account();

            account.Credit(balanceAmount);

            Assert.Throws<EInsufficientBalanceToOperationException>(() => account.Debit(debitAmount));
        }

        [Fact]
        public void Debit_GivenAccountWithValidBalance_ShoudDebitAsExpected()
        {
            var account = new Account();

            account.Credit(100.85m);
            account.Debit(50.32m);

            ValidateCurrentBalanceAccount(account, 50.53m); 
        }

        [Fact]
        public void GetCurrentBalance_GivenAccountWithCreditAndDebitTransactions_ShouldCalculateCurrentBalanceAsExpected()
        {
            var account = new Account();

            account.Credit(100.84m);
            account.Credit(10m);
            account.Debit(84.72m);
            account.Credit(500.98m);
            account.Debit(259.20m);

            ValidateCurrentBalanceAccount(account, 267.90m);
        }

        [Fact]
        public void GetCurrentBalance_GivenAccountWithoutTransactions_ShouldReturnZero()
        {
            var account = new Account();

            ValidateCurrentBalanceAccount(account, 0m);
        }

        private void ValidateCurrentBalanceAccount(Account account, decimal expectedBalance)
        {
            account.GetCurrentBalance().Should().Be(expectedBalance);
        }
    }
}
