namespace bank_account_ddd_studies.domain.query
{
    public class SearchAccountById : IQuery
    {
        public string Id { get; }

        public string Operation => "SearchAccountById";

        public SearchAccountById(string id)
        {
            Id = id;
        }
    }
}
