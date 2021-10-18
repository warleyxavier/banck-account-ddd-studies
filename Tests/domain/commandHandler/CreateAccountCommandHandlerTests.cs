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
    public class CreateAccountCommandHandlerTests
    {
        private readonly CreateAccountCommandHandler _handler;
        private readonly Fixture _fixture = new();
        private readonly Mock<IAccountRepository> _accountRepository = new();

        public CreateAccountCommandHandlerTests()
        {
            _handler = new CreateAccountCommandHandler(_accountRepository.Object);
        }

        [Fact]
        public void Execute_GivenACreateAccountCommand_ShouldSaveNewAccountAsExpected()
        {
            var command = _fixture.Create<CreateAccountCommand>();

            _handler.Execute(command);

            _accountRepository.Verify(a => a.Save(It.IsAny<Account>()), Times.Once());
        }

        [Fact]
        public void OperationIdentifier_GivenACommandAndHisHandler_ShouldHasTheSameOperation()
        {
            var command = _fixture.Create<CreateAccountCommand>();

            _handler.Operation.Should().Be(command.Operation);
        }
    }
}
