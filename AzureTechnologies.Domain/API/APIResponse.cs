using System.Text.Json;
using System.Text.Json.Serialization;

namespace AzureTechnologies.Domain.API
{
    public class APIResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public object? Data { get; set; }

        private const string ServerErrorMessage = "An unexpected error occurred while processing the request.";

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public static APIResponse Error(string error)
        {
            return new APIResponse()
            {
                Success = false,
                Message = error
            };
        }

        public static APIResponse InternalServerError()
        {
            return new APIResponse()
            {
                Success = false,
                Message = ServerErrorMessage
            };
        }

        public static APIResponse StatusOk(string? message = null, object? data = null)
        {
            return new APIResponse()
            {
                Data = data,
                Success = true,
                Message = message
            };
        }
    }
}