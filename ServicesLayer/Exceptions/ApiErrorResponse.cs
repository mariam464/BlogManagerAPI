namespace Business.ServicesLayer.Exceptions
{
    public class ApiErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string? Details { get; set; }
        public string Path { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public static ApiErrorResponse Create(int statusCode, string message, string path, string? details = null)
        {
            return new ApiErrorResponse
            {
                StatusCode = statusCode,
                Message = message,
                Path = path,
                Details = details
            };
        }
    }
}
