using System;
using System.Collections.Generic;
using System.Text;

namespace StockTracking.DataAccess.ApiClasses
{
    public class ApiResponse
    {
        public ApiResponseError error { get; set; }
        public ApiResponse()
        {
            this.error = ApiResponseErrorType.OK.CreateResponseError();
        }

        public ApiResponse(ApiResponseErrorType errorType)
        {
            this.error = errorType.CreateResponseError();
        }

        public ApiResponse(ApiResponseError error)
        {
            this.error = error;
        }
    }
    
    public class PaginationResponse : Pagination
    {
        public int rowCount { get; set; }
        public int pagecount { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T data { get; set; }
        public ApiResponse() : base() { }

        public ApiResponse(ApiResponseErrorType errorType) : base(errorType) { }

        public ApiResponse(ApiResponseError error) : base(error) { }

        public ApiResponse(T data)
        {
            this.data = data;
        }
    }

    public class PaginatedApiResponse<T> : ApiResponse<T>
    {
        public PaginatedApiResponse() : base() { }

        public PaginatedApiResponse(ApiResponseErrorType errorType) : base(errorType) { }

        public PaginatedApiResponse(ApiResponseError error) : base(error) { }

        public PaginatedApiResponse(T data, PaginationResponse p) : base(data) { pagination = p; }

        public PaginationResponse pagination { get; set; }
    }
}
