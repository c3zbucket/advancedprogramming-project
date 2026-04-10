using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace GMMWSystem;

public class GMMWDBContext : DbContext
{
    // Create DBSets for records
    public DbSet<Vehicle> Vehicles { get; set; }
    public DbSet<Motorist> Motorists { get; set; }
    public DbSet<Repair> Repairs { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Part> Parts { get; set; }

    // Constructor for Blazor's Dependency Injection
    //public GMMWDBContext(DbContextOptions<GMMWDBContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration Configuration = new ModelConfigurationBuilder
            .AddJsonFile("appsettings.json")
            .Build();

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var appDataPath = Environment.GetFolderPath(folder);
        var dbPath = Path.Combine(appDataPath, Configuration["DbFilename"]);

        optionsBuilder
            .UseSqlite($"DataSource={dbPath}")
            .EnableSensitiveDataLogging()
            .LogTo(x => Debug.WriteLine(x));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Declare which records are primary keys
        modelBuilder.Entity<Vehicle>().HasKey(v => v.ID);
        modelBuilder.Entity<Motorist>().HasKey(m => m.ID);
        modelBuilder.Entity<Repair>().HasKey(r => r.ID);
        modelBuilder.Entity<Part>().HasKey(p => p.ID);
        modelBuilder.Entity<Booking>().HasKey(b => b.ID);

        // Configure relationships (e.g., Vehicle has one Owner)
        modelBuilder.Entity<Vehicle>()
            .HasOne(v => v.owner)
            .WithMany() // Assuming a motorist can have multiple vehicles
            .HasForeignKey("OwnerId"); // EF Core will create this shadow property
    }
}