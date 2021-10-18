namespace bank_account_ddd_studies.domain.query
{
    public class SearchAccountByIdQuery : IQuery
    {
        public string Id { get; }

        public string Operation => "SearchAccountById";

        public SearchAccountByIdQuery(string id)
        {
            Id = id;
        }
    }
}
