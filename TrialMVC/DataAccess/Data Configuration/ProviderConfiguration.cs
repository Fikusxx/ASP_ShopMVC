using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TrialMVC.DataAccess.Data_Models;

namespace TrialMVC.DataAccess.Data_Configuration
{
    public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers");
            builder.HasData(
                new Provider() { Id = 1, Name = "Provider #1" },
                new Provider() { Id = 2, Name = "Provider #2" },
                new Provider() { Id = 3, Name = "Provider #3" });
        }
    }
}
