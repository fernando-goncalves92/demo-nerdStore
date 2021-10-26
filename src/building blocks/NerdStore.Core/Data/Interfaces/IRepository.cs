using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Core.Data.Interfaces
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
