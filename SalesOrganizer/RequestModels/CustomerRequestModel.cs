﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrganizer.RequestModels
{
    public class CustomerRequestModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }
    }
}
