using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GMMWWeb.Data;

using Records;

/**
 * DB Context Class
 */
public class GMMWDBContext : DbContext
{
    // Create DBSets for records
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Motorist> Motorists { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Part> Parts { get; set; }
    public DbSet<TrainingClass> TrainingClass { get; set; }
    public DbSet<Staff> Staff { get; set; }
    public DbSet<SystemUser> Users { get; set; }

    public DbSet<Student> Students { get; set; }
    public DbSet<Lecturer> Lecturers { get; set; }
    public DbSet<Admin> Admins { get; set; }

    // Constructor that accepts DbContextOptions from DI
    public GMMWDBContext(DbContextOptions<GMMWDBContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure if not already configured by DI
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                // Enable verbose logging for debug - remember remove in prod branch
                .EnableSensitiveDataLogging()
                .LogTo(x => Debug.WriteLine(x));
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SystemUser>().HasDiscriminator().IsComplete(false);

        modelBuilder.Entity<SystemUser>()
            .HasOne(u => u.Member)
            .WithOne()
            .HasForeignKey<SystemUser>(u => u.ID);

        // Add debug data for Staff (Students and lecturers)
        modelBuilder.Entity<Student>().HasData(
            new Student { ID = "S001", Name = "Oliver Smith", PhoneNo = "07700900001", Email = "o.smith@outlook.com" },
            new Student { ID = "S002", Name = "George Jones", PhoneNo = "07700900002", Email = "g.jones@gmail.com" },
            new Student { ID = "S003", Name = "Harry Taylor", PhoneNo = "07700900003", Email = "h.taylor@outlook.com" },
            new Student { ID = "S004", Name = "Jack Brown", PhoneNo = "07700900004", Email = "j.brown@gmail.com" },
            new Student
            {
                ID = "S005", Name = "Jacob Williams", PhoneNo = "07700900005", Email = "j.williams@outlook.com"
            },
            new Student { ID = "S006", Name = "Noah Davies", PhoneNo = "07700900006", Email = "n.davies@gmail.com" }
        );

        modelBuilder.Entity<Lecturer>().HasData(
            new Lecturer
                { ID = "L001", Name = "Alice Johnson", PhoneNo = "07700900101", Email = "a.johnson@gmail.com" },
            new Lecturer
                { ID = "L002", Name = "Rob Youblind", PhoneNo = "07700900102", Email = "r.youblind@outlook.com" }
        );
    }
}