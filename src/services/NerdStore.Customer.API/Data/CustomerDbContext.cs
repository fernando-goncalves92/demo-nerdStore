using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Customer.API.Entities;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.Data
{
    public class CustomerDbContext : DbContext, IUnitOfWork
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Entities.Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            EnsureNVarcharColumnsWillNotBeCreate(modelBuilder);

            EnsureNotDeleteInCascade(modelBuilder);

            ApplyMappings(modelBuilder, typeof(CustomerDbContext).Assembly);
        }

        private void EnsureNVarcharColumnsWillNotBeCreate(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("VARCHAR(100)");
            }
        }

        private void EnsureNotDeleteInCascade(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder
                .Model
                .GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }
        }

        private void ApplyMappings(ModelBuilder modelBuilder, Assembly assembly)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        public async Task<bool> CommitAsync()
        {
            return await base.SaveChangesAsync() > 0;
        }
    }
}
