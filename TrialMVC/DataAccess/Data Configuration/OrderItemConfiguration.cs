using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrialMVC.DataAccess.Data_Models;


namespace TrialMVC.DataAccess.Data_Configuration
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");
            builder.Property(x => x.Quantity).HasPrecision(18, 3);
        }
    }
}
