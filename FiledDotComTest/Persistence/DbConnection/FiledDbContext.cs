using FiledDotComTest.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace FiledDotComTest.Persistence.DbConnection
{
    public class FiledDbContext : DbContext
    {
        public FiledDbContext(DbContextOptions<FiledDbContext> options) : base(options)
        {

        }

        //Entities
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentState> PaymentStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //To list the configuration one by one:
            //modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
            //Below will automatically configure all configurations for you
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<Payment>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<PaymentState>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Seed();
        }

        //Extending the SaveChanges methods
        public override int SaveChanges()
        {
            CreateDateStatuses();
            UpdateDateStatuses();
            UpdateSoftDeleteStatuses();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            CreateDateStatuses();
            UpdateDateStatuses();
            UpdateSoftDeleteStatuses();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void CreateDateStatuses()
        {
            foreach (var entry in this.ChangeTracker.Entries()
            .Where(e => e.Properties.Any(p => p.Metadata.Name == "CreatedDate")
                 && e.State == Microsoft.EntityFrameworkCore.EntityState.Added))
            {
                entry.Property("CreatedDate").CurrentValue = DateTime.Now;
            }
        }

        private void UpdateDateStatuses()
        {
            foreach (var entry in this.ChangeTracker.Entries()
            .Where(e => e.Properties.Any(p => p.Metadata.Name == "UpdatedDate")
                 && e.State != Microsoft.EntityFrameworkCore.EntityState.Added))
            {
                entry.Property("UpdatedDate").CurrentValue = DateTime.Now;
            }
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in this.ChangeTracker.Entries()
            .Where(e => e.Properties.Any(p => p.Metadata.Name == "IsDeleted")
                 && e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted))
            {
                entry.State = EntityState.Modified;
                entry.Property("IsDeleted").CurrentValue = true;
            }
        }
    }
}
