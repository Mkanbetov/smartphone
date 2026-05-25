using Microsoft.Identity.Client;

namespace Smartphone.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public int Phone { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}