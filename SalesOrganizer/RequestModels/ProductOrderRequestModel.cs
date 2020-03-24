namespace SalesOrganizer.RequestModels
{
    public class ProductOrderRequestModel
    {
        public bool Paid { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
    }
}