using BikeDistributor.Domain.SeedWork;
using BikeDistributor.Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BikeDistributor.Infrastructure.Domain
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BikeDistributorContext context;

        public UnitOfWork(BikeDistributorContext context)
        {
            this.context = context;
        }

        public IQueryable<T> Query<T>()
        {
            //You do not need to implement this method for the coding exercise
            throw new System.NotImplementedException();
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
