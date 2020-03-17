using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.DataModels
{
    public class Order
    {
        public int OrderId { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
