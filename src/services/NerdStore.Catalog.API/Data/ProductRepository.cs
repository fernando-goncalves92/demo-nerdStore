using NerdStore.Catalog.API.Entities;
using NerdStore.Core.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalog.API.Data
{
    public class ProductRepository : IProductRepository
    {
        public IUnitOfWork UnitOfWork => _context;

        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public Task<IEnumerable<Product>> GetAll()
        {
            return Task.FromResult<IEnumerable<Product>>(_context.Products.ToList());
        }

        public async Task<Product> GetById(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
