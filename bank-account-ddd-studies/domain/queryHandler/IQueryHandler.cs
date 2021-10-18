using bank_account_ddd_studies.domain.query;

namespace bank_account_ddd_studies.domain.queryHandler
{
    public interface IQueryHandler
    {
        public string Operation { get; }

        T Search<T>(IQuery query);
    }
}
