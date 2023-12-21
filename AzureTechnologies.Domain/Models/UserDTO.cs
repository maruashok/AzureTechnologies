using System.Text.Json.Serialization;

namespace AzureTechnologies.Domain.Models
{
    public class UserDTO
    {
        [JsonPropertyName("fullName")]
        public string FullName { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("password")]
        public string Password { get; set; } = null!;
    }
}