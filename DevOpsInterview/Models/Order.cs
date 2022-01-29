using System.Collections.Generic;

namespace DevOpsInterview.Models
{
    public class Order : IModel
    {
        public int Id { get; set; }
        public int Customer { get; set; } 
        public IList<int> Products { get; set; } 
    }
}