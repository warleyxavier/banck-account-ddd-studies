namespace bank_account_ddd_studies.domain.command
{
    public class CreditCommand : ICommand
    {
        public string AccountId { get; }
        public decimal Amount { get; }

        public string Operation => "Credit";

        public CreditCommand(string accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
