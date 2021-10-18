using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.entity;
using bank_account_ddd_studies.domain.query;
using bank_account_ddd_studies.domain.repository;
using bank_account_ddd_studies.infra.publisher;
using System;

namespace bank_account_ddd_studies.application.service
{
    public class AccountService
    {
        private readonly CommandPublisher _commandPublisher;
        private readonly QueryPublisher _queryPublisher;

        public AccountService(CommandPublisher commandPublisher, QueryPublisher queryPublisher)
        {
            _commandPublisher = commandPublisher;
            _queryPublisher = queryPublisher;
        }

        public void Create()
        {
            var accountId = Guid.NewGuid().ToString();

            var command = new CreateAccountCommand(accountId);

            _commandPublisher.Publish(command);
        }

        public void Credit(string accountId, decimal amount)
        {
            var command = new CreditCommand(accountId, amount);

            _commandPublisher.Publish(command);
        }

        public void Debit(string accountId, decimal amount)
        {
            var command = new DebitCommand(accountId, amount);

            _commandPublisher.Publish(command);
        }

        public void Transfer(string fromAccountId, string toAccountId, decimal amount)
        {
            var command = new TransferCommand(fromAccountId, toAccountId, amount);

            _commandPublisher.Publish(command);
        }

        public Account Search(string accountId)
        {
            var query = new SearchAccountByIdQuery(accountId);

            return _queryPublisher.Search<Account>(query);
        }
    }
}
