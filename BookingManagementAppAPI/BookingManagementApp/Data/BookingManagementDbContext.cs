using BookingManagementApp.Models;
using Microsoft.EntityFrameworkCore;


namespace BookingManagementApp.Data
{
    public class BookingManagementDbContext : DbContext
    {
        public BookingManagementDbContext(DbContextOptions<BookingManagementDbContext> options) : base(options) { }

        // Add Models to migrate
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> Universities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(e => new {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();

            // One University has many Educations
            modelBuilder.Entity<University>()
                        .HasMany(e => e.Educations)
                        .WithOne(u => u.University)
                        .HasForeignKey(e => e.UniversityGuid)
                        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Education>()
                    .HasOne(e => e.Employee)
                    .WithOne(e => e.Education)
                    .HasForeignKey<Education>(e => e.Guid);

            modelBuilder.Entity<Employee>()
                    .HasOne(e => e.Account)
                    .WithOne(e => e.Employee)
                    .HasForeignKey<Employee>(e => e.Guid);

            modelBuilder.Entity<Employee>()
                       .HasMany(e => e.Bookings)
                       .WithOne(u => u.Employee)
                       .HasForeignKey(e => e.EmployeeGuid)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Room>()
                       .HasMany(e => e.Bookings)
                       .WithOne(u => u.Room)
                       .HasForeignKey(e => e.RoomGuid)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Account>()
                       .HasMany(e => e.AccountRoles)
                       .WithOne(u => u.Account)
                       .HasForeignKey(e => e.AccountGuid)
                       .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Role>()
                      .HasMany(e => e.AccountRoles)
                      .WithOne(u => u.Role)
                      .HasForeignKey(e => e.RoleGuid)
                      .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
