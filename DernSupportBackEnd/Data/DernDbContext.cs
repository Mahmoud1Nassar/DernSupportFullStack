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

        // Add the DbSet for SupportRequestSparePart
        public DbSet<SupportRequestSparePart> SupportRequestSpareParts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensure Identity configurations

            // Configure SupportRequest and User (Many-to-One)
            modelBuilder.Entity<SupportRequest>()
                .HasOne(sr => sr.User)
                .WithMany(u => u.SupportRequests)
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure Quote and SupportRequest (Many-to-One)
            modelBuilder.Entity<Quote>()
                .HasOne(q => q.SupportRequest)
                .WithMany(sr => sr.Quotes)
                .HasForeignKey(q => q.SupportRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configure the many-to-many relationship between SupportRequest and SparePart
            modelBuilder.Entity<SupportRequestSparePart>()
                .HasKey(srsp => new { srsp.SupportRequestId, srsp.SparePartId });  // Composite primary key

            modelBuilder.Entity<SupportRequestSparePart>()
                .HasOne(srsp => srsp.SupportRequest)
                .WithMany(sr => sr.SupportRequestSpareParts)
                .HasForeignKey(srsp => srsp.SupportRequestId);

            modelBuilder.Entity<SupportRequestSparePart>()
                .HasOne(srsp => srsp.SparePart)
                .WithMany(sp => sp.SupportRequestSpareParts)
                .HasForeignKey(srsp => srsp.SparePartId);

            // Set precision for decimal properties
            modelBuilder.Entity<Quote>()
                .Property(q => q.TotalCost)
                .HasPrecision(18, 2);

            modelBuilder.Entity<SparePart>()
                .Property(sp => sp.Cost)
                .HasPrecision(18, 2);

            // Seed Identity Roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "1",
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                    Id = "2",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER"
                },
                new IdentityRole
                {
                    Id = "3",
                    Name = "Technician",
                    NormalizedName = "TECHNICIAN"
                }
            );

            // Seed Admin User
            var adminId = "1";
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
                    RoleId = "1",
                    UserId = adminId
                }
            );

            // Seed Technician User
            var technicianId = "2";
            var technicianUser = new ApplicationUser
            {
                Id = technicianId,
                UserName = "technician@example.com",
                NormalizedUserName = "TECHNICIAN@EXAMPLE.COM",
                Email = "technician@example.com",
                NormalizedEmail = "TECHNICIAN@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "Technician User",
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            technicianUser.PasswordHash = passwordHasher.HashPassword(technicianUser, "Technician@123");

            modelBuilder.Entity<ApplicationUser>().HasData(technicianUser);

            // Assign Technician User to Role
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "3",
                    UserId = technicianId
                }
            );
        }
    }
}
