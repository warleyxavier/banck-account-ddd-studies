namespace bank_account_ddd_studies.domain.command
{
    public class DebitCommand : ICommand
    {
        public string AccountId { get; }
        public decimal Amount { get; set; }

        public string Operation => "Debit";

        public DebitCommand(string accountId, decimal amount)
        {
            AccountId = accountId;
            Amount = amount;
        }
    }
}
