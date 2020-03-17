using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SalesOrganizer.DataModels
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
