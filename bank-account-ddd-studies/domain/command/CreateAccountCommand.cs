using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bank_account_ddd_studies.domain.command
{
    public class CreateAccountCommand : ICommand
    {
        public string AccountId { get; }

        public string Operation => "CreateAccount";

        public CreateAccountCommand(string accountId)
        {
            AccountId = accountId;
        }
    }
}
