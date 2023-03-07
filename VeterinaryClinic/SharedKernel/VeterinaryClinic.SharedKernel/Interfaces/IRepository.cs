using Ardalis.Specification;
using System.Collections.Generic;
using VeterinaryClinic.SharedKernel.Interfaces;

namespace VeterinaryClinic.SharedKernel.Interfaces
{
    public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}