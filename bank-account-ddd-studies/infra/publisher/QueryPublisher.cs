using bank_account_ddd_studies.domain.query;
using bank_account_ddd_studies.domain.queryHandler;
using bank_account_ddd_studies.infra.exception;
using System.Collections.Generic;
using System.Linq;

namespace bank_account_ddd_studies.infra.publisher
{
    public class QueryPublisher
    {
        private readonly List<IQueryHandler> _handlers;

        public QueryPublisher()
        {
            _handlers = new List<IQueryHandler>();
        }

        public void Register(IQueryHandler handler)
        {
            ValidateIfHandlerAlreadyRegistered(handler);

            _handlers.Add(handler);
        }

        private void ValidateIfHandlerAlreadyRegistered(IQueryHandler handler)
        {
            var IsAlreadyRegistered = _handlers.Any(h => h.Operation == handler.Operation);

            if (IsAlreadyRegistered)
            {
                throw new EHandlerAlreadyRegisteredException();
            }
        }

        public T Search<T>(IQuery query)
        {
            var handler = _handlers.FirstOrDefault(h => h.Operation == query.Operation);

            if (handler is null)
            {
                throw new EHandlerNotFoundException();
            }

            return handler.Search<T>(query);
        }
    }
}
