using System;
using System.Collections.Generic;

namespace StockTracking.DataAccess.ApiClasses
{
    public static class ApiResponseExtensions
    {
        public static ApiResponseError CreateResponseError(this ApiResponseErrorType errorType) => new ApiResponseError(errorType);

        public static ApiResponse CreateResponse(this ApiResponseErrorType errorType) => new ApiResponse(errorType);

        public static ApiResponse CreateResponse(this ApiResponseError error) => new ApiResponse(error);

        public static ApiResponse CreateResponse(this string message) => new ApiResponseError(message).CreateResponse();

        public static ApiResponse CreateResponse<T>(this ApiResponseErrorType errorType, T data) => new ApiResponse<T>(data);

        public static ApiResponse CreateResponse<T>(this PaginationResponse p, T data) => new PaginatedApiResponse<T>(data, p);
    }

    public enum ApiResponseErrorType
    {
        UnknownError = -1,
        OK = 0,
        AuthenticationFailed = 300,
        
    }

    public class ApiResponseError
    {
        static Dictionary<ApiResponseErrorType, string> errorMessages = new Dictionary<ApiResponseErrorType, string>()
        {
               {ApiResponseErrorType.OK, "Başarılı"},
               {ApiResponseErrorType.AuthenticationFailed, "Giriş başarısız!"},
              
        };

        
            
        public ApiResponseErrorType code { get; set; }
        public string message { get; set; }

        public ApiResponseError(ApiResponseErrorType code)
        {
            this.code = code;
            this.message = errorMessages[this.code];
        }

        public ApiResponseError(string message)
        {
            this.code = ApiResponseErrorType.UnknownError;
            this.message = message;
        }

    }
}
