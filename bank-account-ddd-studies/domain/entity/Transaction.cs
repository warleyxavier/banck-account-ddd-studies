using bank_account_ddd_studies.domain.enumerator;

namespace bank_account_ddd_studies.domain.entity
{
    public class Transaction
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }

        public Transaction(TransactionType type, decimal amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
