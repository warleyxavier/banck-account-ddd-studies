using bank_account_ddd_studies.domain.query;
using bank_account_ddd_studies.domain.queryHandler;
using bank_account_ddd_studies.infra.exception;
using bank_account_ddd_studies.infra.publisher;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.infra.publisher
{
    public class QueryPublisherTests
    {
        private readonly Mock<IQueryHandler> _searchAccountHandler;
        private readonly Mock<IQueryHandler> _searchCustomerHandler;

        private readonly QueryPublisher _publisher;

        public QueryPublisherTests()
        {
            _searchAccountHandler = BuildQueryHandlerMock("SearchAccount");
            _searchCustomerHandler = BuildQueryHandlerMock("SearchCustomer");

            _publisher = BuildPublisher();
        }

        private QueryPublisher BuildPublisher()
        {
            var publisher = new QueryPublisher();

            publisher.Register(_searchAccountHandler.Object);
            publisher.Register(_searchCustomerHandler.Object);

            return publisher;
        }

        [Fact]
        public void RegisterHandler_GivenAAlreadyRegisteredQueryHandler_ShouldThrowEHandlerAlreadyRegisteredException()
        {
            var handlerToRegister = BuildQueryHandlerMock("SearchAccount");

            Assert.Throws<EHandlerAlreadyRegisteredException>(() => _publisher.Register(handlerToRegister.Object));
        }

        [Fact]
        public void RegisterHandler_GivenANonRegisteredQueryHandler_ShouldNotThrowException()
        {
            var handlerToRegister = BuildQueryHandlerMock("xpto");

            var exception = Record.Exception(() => _publisher.Register(handlerToRegister.Object));

            exception.Should().BeNull();
        }

        [Fact]
        public void Search_GivenACommandWithHandlerRegistered_ShouldSearchAsExpected()
        {
            var query = BuildQueryMock("SearchAccount");

            _searchAccountHandler.Setup(h => h.Search<string>(query.Object)).Returns("xpto");

            var queryResult = _publisher.Search<string>(query.Object);

            queryResult.Should().Be("xpto");

            _searchAccountHandler.Verify(h => h.Search<string>(query.Object), Times.Once());
            _searchCustomerHandler.Verify(h => h.Search<string>(query.Object), Times.Never());
        }

        [Fact]
        public void Search_GivenACommandWithoutRegisteredHandler_ShouldThrowEHandlerNotFoundException()
        {
            var query = BuildQueryMock("SearchProduct");

            Assert.Throws<EHandlerNotFoundException>(() => _publisher.Search<string>(query.Object));
        }

        private Mock<IQuery> BuildQueryMock(string operation)
        {
            var result = new Mock<IQuery>();
            result.Setup(q => q.Operation).Returns(operation);
            return result;
        }

        private Mock<IQueryHandler> BuildQueryHandlerMock(string operation)
        {
            var result = new Mock<IQueryHandler>();
            result.Setup(q => q.Operation).Returns(operation);
            return result;
        }
    }
}
