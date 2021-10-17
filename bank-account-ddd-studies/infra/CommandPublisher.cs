using bank_account_ddd_studies.domain.command;
using bank_account_ddd_studies.domain.commandHandler;
using System.Collections.Generic;

namespace bank_account_ddd_studies.infra
{
    public class CommandPublisher
    {
        private readonly List<IHandler> _handlers;

        public CommandPublisher()
        {
            _handlers = new List<IHandler>();
        }

        public void Register(IHandler handler)
        {
            _handlers.Add(handler);
        } 

        public void Publish(ICommand command)
        {
            foreach (var handler in _handlers)
            {
                if (handler.Operation == command.Operation)
                {
                    handler.Execute(command);
                }
            }
        }
    }
}
