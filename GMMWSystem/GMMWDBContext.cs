using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace GMMWSystem;

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
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration Configuration = new ConfigurationBuilder()
            .AddJsonFile("settings.json")
            .Build();

        var folder = Environment.SpecialFolder.LocalApplicationData;
        var appDataPath = Environment.GetFolderPath(folder);
        var dbPath = Path.Combine(appDataPath, Configuration["DbFilename"]);

        optionsBuilder
            .UseSqlite($"DataSource={dbPath}")
            // Enable verbose logging for debug - remember remove in prod branch
            .EnableSensitiveDataLogging() 
            .LogTo(x => Debug.WriteLine(x));
    }
}