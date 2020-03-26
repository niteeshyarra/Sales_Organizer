namespace SalesOrganizer.ResponseModels
{
    public class ProductOrderResponseModel
    {
        public bool Paid { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public ProductResponseModel Product { get; set; }
        public OrderResponseModelExcludeProductOrders Order { get; set; } 
    }
}