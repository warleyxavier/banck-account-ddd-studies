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
    public class DebitHandlerTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<IAccountRepository> _accountRepository = new();
        private readonly DebitHandler _handler;

        public DebitHandlerTests()
        {
            _handler = new DebitHandler(_accountRepository.Object);
        }

        [Fact]
        public void Execute_GivenACommandWithExistingAccount_ShouldExecuteAsExpected()
        {
            var account = new Account();
            account.Credit(2000m);

            var command = new DebitCommand("46587f43h6564", 1500m);

            _accountRepository.Setup(a => a.Get(command.AccountId)).Returns(account);

            _handler.Execute(command);

            account.GetCurrentBalance().Should().Be(500m);
        }

        [Fact]
        public void Execute_GivenACommandWithNonExistentAccount_ShouldNotThrowNullReferenceException()
        {
            var command = _fixture.Create<DebitCommand>();

            _accountRepository.Setup(a => a.Get(command.AccountId)).Returns(null as Account);

            var exception = Record.Exception(() => _handler.Execute(command));

            exception.Should().BeNull();
        }

        [Fact]
        public void OperationIdentifier_GivenACommandAndHisHandler_ShouldHasTheSameOperation()
        {
            var command = _fixture.Create<DebitCommand>();

            _handler.Operation.Should().Be(command.Operation);
        }
    }
}
