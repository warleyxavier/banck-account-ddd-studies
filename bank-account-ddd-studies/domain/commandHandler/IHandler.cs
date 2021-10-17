using bank_account_ddd_studies.domain.command;

namespace bank_account_ddd_studies.domain.commandHandler
{
    public interface IHandler
    {
        string Operation { get; }

        void Execute(ICommand command);
    }
}
