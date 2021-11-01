using NerdStore.Core.Data.Interfaces;
using NerdStore.Customer.API.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.Data
{
    public interface ICustomerRepository : IRepository<Entities.Customer>
    {
        void Add(Entities.Customer customer);
        Task<IEnumerable<Entities.Customer>> GetAll();
        Task<Entities.Customer> GetByCpf(string cpf);
        void AddAddress(Address address);
        Task<Address> GetAddressById(Guid id);
    }
}
