using AutoFixture;
using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.commandHandler;
using bank_account_ddd_studies.domain.entity;
using bank_account_ddd_studies.domain.repository;
using bank_account_ddd_studies.domain.service;
using FluentAssertions;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace Tests.domain.commandHandler
{
    public class TransferHandlerTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IAccountRepository> _accountRepository = new();
        private readonly Mock<ITransferService> _transferService = new();
        private readonly TransferHandler _handler;

        public TransferHandlerTests()
        {
            _handler = new TransferHandler(_accountRepository.Object, _transferService.Object);
        }

        [Fact]
        public void Execute_GivenACommandWithExistingFromAndToAccounts_ShouldExecuteTransferServiceAsExpected()
        {
            var command = _fixture.Create<TransferCommand>();

            var fromAccount = new Account();
            fromAccount.Credit(command.Amount);

            _accountRepository.Setup(a => a.Get(command.FromAccountId)).Returns(fromAccount);
            _accountRepository.Setup(a => a.Get(command.ToAccountId)).Returns(new Account());

            _handler.Execute(command);

            _transferService.Verify(t => t.Transfer(fromAccount, It.IsAny<Account>(), command.Amount), Times.Once());
        }

        [Theory]
        [MemberData(nameof(GenerateFromAccountAndToAccount))]
        public void Execute_GivenACommandWithNonExistentFromAccountOrToAccount_ShouldNotExecuteTransferService(Account fromAccount, Account toAccount)
        {
            var command = _fixture.Create<TransferCommand>();

            _accountRepository.Setup(a => a.Get(command.FromAccountId)).Returns(fromAccount);
            _accountRepository.Setup(a => a.Get(command.ToAccountId)).Returns(toAccount);

            _handler.Execute(command);

            _transferService.Verify(t => t.Transfer(It.IsAny<Account>(), It.IsAny<Account>(), command.Amount), Times.Never());
        }

        public static IEnumerable<object[]> GenerateFromAccountAndToAccount()
        {
            yield return new object[] { null, new Account() };
            yield return new object[] { new Account(), null };
        }

        [Fact]
        public void OperationIdentifier_GivenACommandAndHisHandler_ShouldHasTheSameOperation()
        {
            var command = _fixture.Create<TransferCommand>();

            _handler.Operation.Should().Be(command.Operation);
        }
    }
}
