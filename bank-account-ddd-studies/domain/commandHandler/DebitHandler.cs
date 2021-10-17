using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.repository;

namespace bank_account_ddd_studies.domain.commandHandler
{
    public class DebitHandler : IHandler
    {
        private readonly IAccountRepository _accountRepository;

        public string Operation => "Debit";

        public DebitHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Execute(ICommand command)
        {
            var debitCommand = (DebitCommand)command;

            var account = _accountRepository.Get(debitCommand.AccountId);

            if (account is not null)
            {
                account.Debit(debitCommand.Amount);
            }
        }
    }
}
