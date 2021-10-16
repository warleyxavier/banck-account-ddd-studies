using bank_account_ddd_studies.domain.enumerator;
using bank_account_ddd_studies.domain.exception;
using System.Collections.Generic;
using System.Linq;

namespace bank_account_ddd_studies.domain.entity
{
    public class Account
    {
        public List<Transaction> Transactions { get; }

        public Account()
        {
            Transactions = new List<Transaction>();
        }

        public Transaction Credit(decimal amount)
        {
            var transaction = new Transaction(TransactionType.Credit, amount);

            Transactions.Add(transaction);

            return transaction;
        }

        public Transaction Debit(decimal amount)
        {
            ValidateIfCanDebit(amount);

            var transaction = new Transaction(TransactionType.Debit, amount);

            Transactions.Add(transaction);

            return transaction;
        }

        private void ValidateIfCanDebit(decimal debitAmount)
        {
            var currenteBalance = GetCurrentBalance();

            if (debitAmount > currenteBalance)
            {
                throw new EInsufficientBalanceToOperationException();
            }
        }

        public decimal GetCurrentBalance()
        {
            return Transactions.Aggregate(0m, (balance, transaction) =>
            {
                return (transaction.Type == TransactionType.Credit) ? balance + transaction.Amount : balance - transaction.Amount;
            });
        }
    }
}
