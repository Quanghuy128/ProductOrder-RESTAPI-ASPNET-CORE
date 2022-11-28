namespace ProductOrder.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public long Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
