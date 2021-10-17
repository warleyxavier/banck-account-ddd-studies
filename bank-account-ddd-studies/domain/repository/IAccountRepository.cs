using bank_account_ddd_studies.domain.entity;

namespace bank_account_ddd_studies.domain.repository
{
    public interface IAccountRepository
    {
        Account Get(string Id);
    }
}
