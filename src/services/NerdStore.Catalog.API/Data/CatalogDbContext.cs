using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.API.Entities;
using NerdStore.Core.Data.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalog.API.Data
{
    public class CatalogDbContext : DbContext, IUnitOfWork
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EnsureNVarcharColumnsWillNotBeCreate(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogDbContext).Assembly);
        }

        private void EnsureNVarcharColumnsWillNotBeCreate(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
