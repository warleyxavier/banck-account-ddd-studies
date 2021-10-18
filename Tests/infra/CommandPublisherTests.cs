using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.commandHandler;
using bank_account_ddd_studies.infra.publisher;
using Moq;
using Xunit;

namespace Tests.infra
{
    public class CommandPublisherTests
    {
        private readonly Mock<IHandler> _creditHandler = new();
        private readonly Mock<IHandler> _deditHandler = new();

        private readonly CommandPublisher _publisher;

        public CommandPublisherTests()
        {
            _creditHandler.Setup(c => c.Operation).Returns("Credit");
            _deditHandler.Setup(c => c.Operation).Returns("Debit");

            _publisher = BuildPublisher();
        }

        private CommandPublisher BuildPublisher()
        {
            var publisher = new CommandPublisher();

            publisher.Register(_creditHandler.Object);
            publisher.Register(_deditHandler.Object);

            return publisher;
        }

        [Fact]
        public void Publish_GivenACreditCommandWithRegisteredHandler_ShouldPublishAsExpected()
        {
            var creditCommand = new Mock<ICommand>();
            creditCommand.Setup(c => c.Operation).Returns("Credit");

            _publisher.Publish(creditCommand.Object);

            _creditHandler.Verify(c => c.Execute(creditCommand.Object), Times.Once());
            _deditHandler.Verify(c => c.Execute(It.IsAny<ICommand>()), Times.Never());
        }

        [Fact]
        public void Publish_GivenADebitCommandWithRegisteredHandler_ShouldPublishAsExpected()
        {
            var creditCommand = new Mock<ICommand>();
            creditCommand.Setup(c => c.Operation).Returns("Debit");

            _publisher.Publish(creditCommand.Object);

            _deditHandler.Verify(c => c.Execute(creditCommand.Object), Times.Once());
            _creditHandler.Verify(c => c.Execute(It.IsAny<ICommand>()), Times.Never());
        }

        [Fact]
        public void Publish_GivenACommandWithoutRegisteredCommand_ShouldNotPublish()
        {
            var creditCommand = new Mock<ICommand>();
            creditCommand.Setup(c => c.Operation).Returns("Transfer");

            _publisher.Publish(creditCommand.Object);

            _deditHandler.Verify(c => c.Execute(It.IsAny<ICommand>()), Times.Never());
            _creditHandler.Verify(c => c.Execute(It.IsAny<ICommand>()), Times.Never());
        }
    }
}
