using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.Results;
using NerdStore.Core.Mediator;
using NerdStore.Core.Messages;
using NerdStore.Core.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using NerdStore.Order.Domain.Voucher;
using NerdStore.Order.Infra.Extensions;
using System;

namespace NerdStore.Order.Infra.Data
{
    public class OrderDbContext : DbContext, IUnitOfWork
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderDbContext(DbContextOptions<OrderDbContext> options, IMediatorHandler mediatorHandler)
            : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;

            _mediatorHandler = mediatorHandler;
        }

        public DbSet<Domain.Order.Order> Orders { get; set; }
        public DbSet<Domain.Order.OrderItem> OrderItems { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<ValidationResult>();
            modelBuilder.Ignore<Event>();

            EnsureNVarcharColumnsWillNotBeCreate(modelBuilder);

            EnsureNotDeleteInCascade(modelBuilder);

            EnsureSequenceIdStartsIn1000(modelBuilder);

            ApplyMappings(modelBuilder, typeof(OrderDbContext).Assembly);
        }

        private void EnsureSequenceIdStartsIn1000(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasSequence<int>("MySequenceStartingIn1000")
                .StartsAt(1000)
                .IncrementsBy(1);
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

        private void EnsureNotChangeRegistrationDateFields()
        {
            foreach (var entry in ChangeTracker
                .Entries()
                .Where(entry => entry.Entity.GetType().GetProperty("RegistrationDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("RegistrationDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("RegistrationDate").IsModified = false;
                }
            }
        }

        private void ApplyMappings(ModelBuilder modelBuilder, Assembly assembly)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        }

        public async Task<bool> CommitAsync()
        {
            EnsureNotChangeRegistrationDateFields();

            var success = await base.SaveChangesAsync() > 0;

            if (success)
            {
                await _mediatorHandler.PublishEvents(this);
            }

            return success;
        }
    }
}
