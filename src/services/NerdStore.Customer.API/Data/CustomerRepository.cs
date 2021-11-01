using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Customer.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public void Add(Entities.Customer customer)
        {
            _context.Customers.Add(customer);
        }

        public void AddAddress(Address address)
        {
            _context.Addresses.Add(address);
        }

        public Task<Address> GetAddressById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Entities.Customer>> GetAll()
        {
            return await _context.Customers.AsNoTracking().ToListAsync();
        }

        public Task<Entities.Customer> GetByCpf(string cpf)
        {
            return _context.Customers.FirstOrDefaultAsync(c => c.Cpf.Number == cpf);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
