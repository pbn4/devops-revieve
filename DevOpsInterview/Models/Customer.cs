namespace DevOpsInterview.Models
{
    public class Customer : IModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}