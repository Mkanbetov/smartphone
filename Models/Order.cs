namespace Smartphone.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public string CustomerName { get; set; }

        public Customer Customer { get; set; }
        public Product Product { get; set; }
    }
}
