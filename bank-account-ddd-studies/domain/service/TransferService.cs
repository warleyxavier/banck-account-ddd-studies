using bank_account_ddd_studies.domain.entity;

namespace bank_account_ddd_studies.domain.service
{
    public class TransferService
    {
        public void Transfer(Account from, Account to, decimal amount)
        {
            from.Debit(amount);
            to.Credit(amount);
        }
    }
}
