using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.repository;
using bank_account_ddd_studies.domain.service;

namespace bank_account_ddd_studies.domain.commandHandler
{
    public class TransferHandler : IHandler
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransferService _transferService;

        public string Operation => "Transfer";

        public TransferHandler(IAccountRepository accountRepository, ITransferService transferService)
        {
            _accountRepository = accountRepository;
            _transferService = transferService;
        }

        public void Execute(ICommand command)
        {
            var transferCommand = (TransferCommand)command;

            var fromAccount = _accountRepository.Get(transferCommand.FromAccountId);
            var toAccount = _accountRepository.Get(transferCommand.ToAccountId);

            if (fromAccount is not null && toAccount is not null)
            {
                _transferService.Transfer(fromAccount, toAccount, transferCommand.Amount);
            }
        }
    }
}
