using Microsoft.EntityFrameworkCore;
using TrialMVC.DataAccess.Data_Configuration;

namespace TrialMVC.DataAccess.Data_Models
{
    [EntityTypeConfiguration(typeof(OrderItemConfiguration))]
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
