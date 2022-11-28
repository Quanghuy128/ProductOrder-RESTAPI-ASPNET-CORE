namespace ProductOrder.Models
{
    public class UpdateAccountRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
