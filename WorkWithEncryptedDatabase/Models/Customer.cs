using System.Text.Json.Serialization;

namespace WorkWithEncryptedDatabase
{
    public class Customer
    {
        public Customer(int id, string firstName = null, string lastName = null)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
    }
}
