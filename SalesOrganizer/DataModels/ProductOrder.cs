﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.DataModels
{
    public class ProductOrder
    {
        public int ProductOrderId { get; set; }
        [Required]
        public int Quantity { get; set; }
        public bool Paid { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
    }
}
