using AutoFixture;
using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.commandHandler;
using bank_account_ddd_studies.domain.entity;
using bank_account_ddd_studies.domain.repository;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.domain.commandHandler
{
    public class CreditHandlerTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IAccountRepository> _accountRepository = new();
        private readonly CreditHandler _handler;

        public CreditHandlerTests()
        {
            _handler = new CreditHandler(_accountRepository.Object);
        }

        [Fact]
        public void Execute_GivenACommandWithExistingAccount_ShouldExecuteAsExpected()
        {
            var account = new Account();
            var command = _fixture.Create<CreditCommand>();

            _accountRepository.Setup(a => a.Get(command.AccountId)).Returns(account);

            _handler.Execute(command);

            account.GetCurrentBalance().Should().Be(command.Amount);
        }

        [Fact]
        public void Execute_GivenACommandWithNonExistentAccount_ShouldNotThrowNullReferenceException()
        {
            var command = _fixture.Create<CreditCommand>();

            _accountRepository.Setup(a => a.Get(command.AccountId)).Returns(null as Account);

            var exception = Record.Exception(() => _handler.Execute(command));

            exception.Should().BeNull();
        }

        [Fact]
        public void OperationIdentifier_GivenACommandAndHisHandler_ShouldHasTheSameOperation()
        {
            var command = _fixture.Create<CreditCommand>();

            _handler.Operation.Should().Be(command.Operation);
        }
    }
}
