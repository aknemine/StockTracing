namespace StockTracking.DataAccess.ApiClasses
{
    public class Pagination
    {
        public int Page { get; set; }
        public int RowsPerPage { get; set; }
    }

    public class ApiRequest
    {
    }

    public class PaginatedApiRequest : ApiRequest
    {
        public Pagination Pagination { get; set; }
    }

    public class PaginatedApiRequest<T> : PaginatedApiRequest
    {
        public T Data { get; set; }
    }
}
