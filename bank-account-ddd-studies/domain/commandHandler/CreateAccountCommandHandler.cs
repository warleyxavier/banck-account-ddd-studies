using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.entity;
using bank_account_ddd_studies.domain.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_account_ddd_studies.domain.commandHandler
{
    public class CreateAccountCommandHandler : IHandler
    {
        private readonly IAccountRepository _repository;

        public string Operation => "CreateAccount";

        public CreateAccountCommandHandler(IAccountRepository repository)
        {
            _repository = repository;
        }

        public void Execute(ICommand command)
        {
            var createAccountCommand = (CreateAccountCommand)command;

            var account = new Account(createAccountCommand.AccountId);

            _repository.Save(account);
        }
    }
}
