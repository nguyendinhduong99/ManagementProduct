using PM.Common.Paging;

namespace PM.Common.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public PagingMetadata Metadata { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public static ApiResponse<T> Fail(string errorMessage)
        {
            return new ApiResponse<T> { Succeeded = false, Message = errorMessage };
        }
        public static ApiResponse<T> Success(T data, PagingMetadata metadata = null)
        {
            return new ApiResponse<T> { Succeeded = true, Data = data, Metadata = metadata };
        }
    }
}
