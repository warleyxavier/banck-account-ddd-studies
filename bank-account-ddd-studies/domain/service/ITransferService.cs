using bank_account_ddd_studies.domain.entity;

namespace bank_account_ddd_studies.domain.service
{
    public interface ITransferService
    {
        void Transfer(Account from, Account to, decimal amount);
    }
}
