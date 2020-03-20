using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.ViewModels
{
    public class OrderViewModel
    {
        public DateTime OrderDate { get; set; }
        public int CustomerId { get; set; }
        public ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
