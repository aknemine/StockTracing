namespace StockTracking.DataAccess.ApiClasses
{
    public class Pagination
    {
        public int page { get; set; }
        public int rowsPerPage { get; set; }
    }
    public class ApiRequest
    {
    }
    public class PaginatedApiRequest : ApiRequest
    {
        public Pagination pagination { get; set; }
    }

    public class PaginatedApiRequest<T> : PaginatedApiRequest
    {
        public T data { get; set; }
    }
}
