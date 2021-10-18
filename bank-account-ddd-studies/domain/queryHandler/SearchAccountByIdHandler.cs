using bank_account_ddd_studies.domain.query;
using bank_account_ddd_studies.domain.repository;
using System;

namespace bank_account_ddd_studies.domain.queryHandler
{
    public class SearchAccountByIdHandler : IQueryHandler
    {
        private readonly IAccountRepository _accountRepository;

        public string Operation => "SearchAccountById";

        public SearchAccountByIdHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public T Search<T>(IQuery query)
        {
            var searchAccount = (SearchAccountById)query;

            var account = _accountRepository.Get(searchAccount.Id) is T;

            return (T)Convert.ChangeType(account, typeof(T));
        }
    }
}
