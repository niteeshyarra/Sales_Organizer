using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sales_Organizer.Data_Models
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
