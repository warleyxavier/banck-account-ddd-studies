using AutoFixture;
using bank_account_ddd_studies.domain.entity;
using bank_account_ddd_studies.domain.query;
using bank_account_ddd_studies.domain.queryHandler;
using bank_account_ddd_studies.domain.repository;
using FluentAssertions;
using Moq;
using Xunit;

namespace Tests.domain.queryHandler
{
    public class SearchAccountByIdHandlerTests
    {
        private readonly SearchAccountByIdHandler _handler;
        private readonly Fixture _fixture = new();
        private readonly Mock<IAccountRepository> _repository = new();

        public SearchAccountByIdHandlerTests()
        {
            _handler = new SearchAccountByIdHandler(_repository.Object);
        }

        [Fact]
        public void Search_GivenAccountFound_ShouldReturnObjectAsExpected()
        {
            var account = new Account();

            var query = _fixture.Create<SearchAccountByIdQuery>();

            _repository.Setup(r => r.Get(query.Id)).Returns(account);

            var accountSearched = _handler.Search<Account>(query);

            accountSearched.Should().Be(account);
        }

        [Fact]
        public void Search_GivenAccountNotFound_ShouldReturnNullAsExpected()
        {
            var query = _fixture.Create<SearchAccountByIdQuery>();

            _repository.Setup(r => r.Get(query.Id)).Returns(null as Account);

            var accountSearched = _handler.Search<Account>(query);

            accountSearched.Should().BeNull();
        }

        [Fact]
        public void OperationIdentifier_GivenAQueryAndHisHandler_ShouldHasTheSameOperation()
        {
            var query = _fixture.Create<SearchAccountByIdQuery>();

            _handler.Operation.Should().Be(query.Operation);
        }
    }
}
