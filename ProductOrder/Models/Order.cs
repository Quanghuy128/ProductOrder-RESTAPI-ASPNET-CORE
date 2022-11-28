namespace ProductOrder.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; }
        public string AccountId { get; set; }
    }
}
