namespace WorkWithEncryptedDatabase
{
    public class PostCustomerRequest : Customer
    {
        public PostCustomerRequest(string firstName, string lastName)
            : base(default, firstName, lastName)
        {
        }
    }
}
