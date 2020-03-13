using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales_Organizer.Data_Models
{
    public class ProductInOrder
    {
        public int ProductInOrderId { get; set; }
        public int Quantity { get; set; }
        public bool Paid { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
