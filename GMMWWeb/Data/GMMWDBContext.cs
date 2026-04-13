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
    public GMMWDBContext(DbContextOptions<GMMWDBContext> options) : base(options) { }
    
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
}
}