using System.ComponentModel.DataAnnotations;

namespace SalesOrganizer.DataModels
{
    public class Product
    {
        public int ProductId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
    }
}
