﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.ResponseModels
{
    public class OrderResponseModel
    {
        public int OrderId { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public int CustomerId { get; set; }
        public CustomerResponseModel Customer { get; set; }
        public ICollection<ProductOrderResponseModel> ProductOrders { get; set; }
    }
}
