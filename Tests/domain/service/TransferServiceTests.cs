using bank_account_ddd_studies.domain.entity;
using bank_account_ddd_studies.domain.service;
using FluentAssertions;
using Xunit;

namespace Tests.domain.service
{
    public class TransferServiceTests
    {
        [Fact]
        public void TransferBalance_GivenTwoAccounts_ShouldTransferAsExpected()
        {
            const decimal transferValue = 250.61m;

            var fromAccount = new Account();
            fromAccount.Credit(500.90m);

            var toAccount = new Account();

            var transferService = new TransferService();

            transferService.Transfer(fromAccount, toAccount, transferValue);

            ValidateCurrentBalanceAccount(fromAccount, 250.29m);
            ValidateCurrentBalanceAccount(toAccount, transferValue);
        }

        private void ValidateCurrentBalanceAccount(Account account, decimal expectedBalance)
        {
            account.GetCurrentBalance().Should().Be(expectedBalance);
        }
    }
}
