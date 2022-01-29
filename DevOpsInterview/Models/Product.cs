namespace DevOpsInterview.Models
{
    public class Product : IModel
    {
       public int Id { get; set; } 
       public string Name { get; set; } 
       public decimal Cost { get; set; } 
    }
}