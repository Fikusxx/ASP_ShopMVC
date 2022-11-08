using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrialMVC.DataAccess.Data_Models;


namespace TrialMVC.DataAccess.Data_Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.Property(x => x.Date).HasColumnType("datetime2");
            builder.HasIndex(p => new { p.Number, p.ProviderId }).IsUnique();
        }
    }
}
