using DesafioDev.Core.DomainObjects;

namespace DesafioDev.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
