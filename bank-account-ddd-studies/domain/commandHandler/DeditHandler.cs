using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.repository;

namespace bank_account_ddd_studies.domain.commandHandler
{
    public class DeditHandler : IHandler
    {
        private readonly IAccountRepository _accountRepository;
        
        public string Operation => "Credit";

        public DeditHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Execute(ICommand command)
        {
            var creditCommand = (CreditCommand)command;

            var account = _accountRepository.Get(creditCommand.AccountId);

            if (account is not null)
            {
                account.Credit(creditCommand.Amount);
            }
        }
    }
}
