namespace bank_account_ddd_studies.domain.command
{
    public class TransferCommand : ICommand
    {
        public string FromAccountId { get; }
        public string ToAccountId { get; }
        public decimal Amount { get; }

        public string Operation => "Transfer";

        public TransferCommand(string fromAccountId, string toAccountId, decimal amount)
        {
            FromAccountId = fromAccountId;
            ToAccountId = toAccountId;
            Amount = amount;
        }
    }
}
