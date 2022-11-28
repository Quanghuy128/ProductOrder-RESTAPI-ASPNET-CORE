namespace ProductOrder.Models
{
    public class CreateNewOrderDetailRequest
    {
        public long Price { get; set; }
        public int Quantity { get; set; }
        public string ProductId { get; set; }
        public string OrderId { get; set; }
    }
}
