using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DernSupportBackEnd.Models;
using Microsoft.AspNetCore.Identity;

namespace DernSupportBackEnd.Data
{
    public class DernDbContext : IdentityDbContext<ApplicationUser>
    {
        public DernDbContext(DbContextOptions<DernDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for your entities
        public DbSet<SupportRequest> SupportRequests { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        public DbSet<Quote> Quotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This is important to call Identity configurations

            // Seed Roles: Admin, Customer, Technician
            string adminRoleId = Guid.NewGuid().ToString();
            string customerRoleId = Guid.NewGuid().ToString();
            string technicianRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = customerRoleId,
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Id = technicianRoleId,
                    Name = "Technician",
                    NormalizedName = "TECHNICIAN"
                }
            );

            // Seed Admin User
            var adminId = Guid.NewGuid().ToString();
            var passwordHasher = new PasswordHasher<ApplicationUser>();

            var adminUser = new ApplicationUser
            {
                Id = adminId,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "System Admin",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "Admin@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // Assign Admin User to Role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminId
                }
            );

            // Fluent API Configurations for relationships and complex cases

            // One-to-Many: SupportRequest has many Appointments
            modelBuilder.Entity<SupportRequest>()
                .HasMany(sr => sr.Appointments)
                .WithOne(a => a.SupportRequest)
                .HasForeignKey(a => a.SupportRequestId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete appointments when SupportRequest is deleted

            // Many-to-Many: SupportRequest <-> SparePart
            modelBuilder.Entity<SupportRequest>()
                .HasMany(sr => sr.RequiredSpareParts)
                .WithMany(sp => sp.SupportRequests)
                .UsingEntity(j => j.ToTable("SupportRequestSpareParts")); // This creates a join table

            // One-to-One: SupportRequest has one Quote
            modelBuilder.Entity<SupportRequest>()
                .HasOne(sr => sr.Quote)
                .WithOne(q => q.SupportRequest)
                .HasForeignKey<Quote>(q => q.SupportRequestId)
                .OnDelete(DeleteBehavior.Cascade); // Cascade delete quote when SupportRequest is deleted
        }
    }
}
