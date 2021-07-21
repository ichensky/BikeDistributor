using BikeDistributor.Domain;
using BikeDistributor.Domain.Order;
using Microsoft.EntityFrameworkCore;

namespace BikeDistributor.Infrastructure.Database
{
    public class BikeDistributorContext : DbContext
    {
        public DbSet<Order> Orders => Set<Order>();

        public BikeDistributorContext(DbContextOptions<BikeDistributorContext> options) : base(options) { }
    }
}
