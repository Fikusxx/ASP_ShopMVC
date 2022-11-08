using Microsoft.EntityFrameworkCore;
using TrialMVC.DataAccess.Data_Configuration;

namespace TrialMVC.DataAccess.Data_Models
{

    [EntityTypeConfiguration(typeof(ProviderConfiguration))]
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
