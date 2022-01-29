using System.Collections.Generic;

namespace DevOpsInterview.Models
{
    public class ProductCustomer
    {
        public int Id { get; set; }
        public IEnumerable<int> CustomerIds { get; set; }
    }
}