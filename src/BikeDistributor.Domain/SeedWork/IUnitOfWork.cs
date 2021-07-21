using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BikeDistributor.Domain.SeedWork
{
    /// <summary>
    /// Unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        IQueryable<T> Query<T>();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
