using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.RequestModels
{
    public class OrderRequestModel
    {
        public DateTime CreatedDateTime { get; set; }
        public int CustomerId { get; set; }
        public ICollection<ProductOrderRequestModel> ProductOrders { get; set; }
    }
}
