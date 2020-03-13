﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sales_Organizer.Data_Models
{
    public class Order
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public ICollection<ProductInOrder> ProductsInOrder { get; set; }
    }
}
