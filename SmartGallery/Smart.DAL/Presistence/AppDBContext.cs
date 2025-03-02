using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Smart.Core.Entities.Commons;
using Smart.Core.Entities.Identity;
using Smart.Core.Entities;
using Smart.Shared.Interfaces;

namespace App.DAL.Presistence
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IClaimService _claimService;

        public AppDbContext(DbContextOptions<AppDbContext> options, IClaimService claimService) : base(options)
        {
            _claimService = claimService;
        }

        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Specification> Specifications { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            foreach (var entry in ChangeTracker.Entries<IAuditedEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _claimService.GetUserId();
                        entry.Entity.CreatedOn = DateTime.UtcNow;

                        entry.Entity.UpdatedBy = _claimService.GetUserId();
                        entry.Entity.UpdatedOn = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _claimService.GetUserId();
                        entry.Entity.UpdatedOn = DateTime.UtcNow;
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
