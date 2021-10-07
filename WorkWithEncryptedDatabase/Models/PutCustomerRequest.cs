using System.Text.Json.Serialization;

namespace WorkWithEncryptedDatabase
{
    public class PutCustomerRequest
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
    }
}