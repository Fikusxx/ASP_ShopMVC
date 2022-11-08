using Microsoft.EntityFrameworkCore;
using TrialMVC.DataAccess.Data_Configuration;

namespace TrialMVC.DataAccess.Data_Models
{
    [EntityTypeConfiguration(typeof(OrderConfiguration))]
    public class Order
    {
        public int Id { get; set; }
        public string Number { get; set; } = null!;
        public DateTime Date { get; set; }
        public int ProviderId { get; set; } 
        public Provider Provider { get; set; } = null!;
        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
