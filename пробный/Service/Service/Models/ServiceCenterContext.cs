using System.Data.Entity;

namespace ServiceCenterApp.Models
{
    public class ServiceCenterContext : DbContext
    {
        public ServiceCenterContext()
            : base("name=ServiceCenterDBConnection") { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}