using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using NerdStore.Core.Data.Interfaces;
using NerdStore.Core.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Customer.API.Entities;
using NerdStore.Customer.API.Extensions;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace NerdStore.Customer.API.Data
{
    public class CustomerDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Entities.Customer> Customers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

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
            var success = await base.SaveChangesAsync() > 0;

            if (success)
            {
                await _mediatorHandler.PublishEvents(this);
            }

            return success;
        }
    }
}
