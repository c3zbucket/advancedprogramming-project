using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace GMMWWeb.Data;

using Records;
using Enums;

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


    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<SystemUser>().HasDiscriminator().IsComplete(false);

        mb.Entity<SystemUser>()
            .HasOne(u => u.Member)
            .WithOne()
            .HasForeignKey<SystemUser>(u => u.ID);
        
        mb.Entity<Vehicle>().HasKey(v => v.ID);
        mb.Entity<Vehicle>()
            .HasOne(b => b.owner)
            .WithMany()
            .HasForeignKey(b => b.ownerID);
        
        //mb.Entity<Booking>().HasKey(b => b.ID);
        mb.Entity<Booking>()
            .HasOne(b => b.bookedVehicle)
            .WithMany()
            .HasForeignKey(b => b.bookedPlate);
        
        mb.Entity<Booking>()
            .HasMany(b => b.Repairs)
            .WithOne(r => r.ascBooking)
            .HasForeignKey(r => r.ascID);
        
        mb.Entity<TrainingClass>()
            .HasOne(b => b.student)
            .WithMany()
            .HasForeignKey(b => b.StudentID);

        // Add debug data for Record entries
        mb.Entity<Student>().HasData(
            new Student { ID = "S001", Name = "Oliver Smith", PhoneNo = "07700900001", Email = "o.smith@outlook.com" },
            new Student { ID = "S002", Name = "George Jones", PhoneNo = "07700900002", Email = "g.jones@gmail.com" },
            new Student { ID = "S003", Name = "Harry Taylor", PhoneNo = "07700900003", Email = "h.taylor@outlook.com" },
            new Student { ID = "S004", Name = "Jack Brown", PhoneNo = "07700900004", Email = "j.brown@gmail.com" },
            new Student { ID = "S005", Name = "Jacob Williams", PhoneNo = "07700900005", Email = "j.williams@outlook.com" },
            new Student { ID = "S006", Name = "Noah Davies", PhoneNo = "07700900006", Email = "n.davies@gmail.com" }
        );

        mb.Entity<Lecturer>().HasData(
            new Lecturer { ID = "L001", Name = "Alice Johnson", PhoneNo = "07700900101", Email = "a.johnson@gmail.com" },
            new Lecturer { ID = "L002", Name = "Rob Youblind", PhoneNo = "07700900102", Email = "r.youblind@outlook.com" }
        );

        /*
        mb.Entity<SystemUser>().HasData(
            new Admin { ID = "A001", Name = "Greg Adminny", PhoneNo = "07700900999", Email = "greg.a@outlook.com" }
        );
        */

        mb.Entity<Motorist>().HasData(
            new Motorist { ID = "1001", Name = "Amelia Evans", Email = "amelia.evans@gmail.com", PhoneNo = "07700900011" },
            new Motorist { ID = "1002", Name = "Olivia Wilson", Email = "olivia.w@hotmail.co.uk", PhoneNo = "07700900012" },
            new Motorist { ID = "1003", Name = "Isla Thomas", Email = "isla.thom@gmail.com", PhoneNo = "07700900013" },
            new Motorist { ID = "1004", Name = "Ava Roberts", Email = "ava.r@yahoo.co.uk", PhoneNo = "07700900014" },
            new Motorist { ID = "1005", Name = "Emily Johnson", Email = "emily.j@outlook.com", PhoneNo = "07700900015" },
            new Motorist { ID = "1006", Name = "Mia Wright", Email = "mia.w@gmail.com", PhoneNo = "07700900016" },
            new Motorist { ID = "1007", Name = "Grace Robinson", Email = "grace.rob@gmail.com", PhoneNo = "07700900017" },
            new Motorist { ID = "1008", Name = "Sophia Thompson", Email = "sophia.t@yahoo.co.uk", PhoneNo = "07700900018" }
        );

        mb.Entity<Part>().HasData(
        
            new Part { ID = "P-BOSCH3", Make = "Bosch", Type = PartType.SUSPENSION, Cost = 55.00m, Description = "Brake pads front" },
            new Part { ID = "P-MICHELIN5", Make = "Michelin", Type = PartType.BODY, Cost = 25.00m, Description = "Wiper blade set" },
        
            new Part { ID = "P-NGK1", Make = "NGK", Type = PartType.ENGINE, Cost = 12.50m, Description = "Spark plug" },
            new Part { ID = "P-CASTROL1", Make = "Castrol", Type = PartType.ENGINE, Cost = 35.00m, Description = "5W-30 Engine Oil 5L" },
            new Part { ID = "P-BOSCH2", Make = "Bosch", Type = PartType.ELECTRICAL, Cost = 85.00m, Description = "Alternator" },
            new Part { ID = "P-YUASA2", Make = "Yuasa", Type = PartType.ELECTRICAL, Cost = 110.00m, Description = "12V Car Battery" },
            new Part { ID = "P-BREMBO3", Make = "Brembo", Type = PartType.SUSPENSION, Cost = 140.00m, Description = "Brake disc pair" },
            new Part { ID = "P-MANN1", Make = "Mann", Type = PartType.ENGINE, Cost = 18.00m, Description = "Air filter" }
        );

        mb.Entity<Vehicle>().HasData(
            new { ID = "LN15 XYA", ownerID = "1001", Make = "Ford", Model = "Fiesta", Year = 2015, transmission = Transmission.MANUAL, engine = Engine.PETROL },
            new { ID = "BD51 SMR", ownerID = "1002", Make = "Vauxhall", Model = "Corsa", Year = 2001, transmission = Transmission.MANUAL, engine = Engine.PETROL },
            new { ID = "GL19 ABC", ownerID = "1003", Make = "Volkswagen", Model = "Golf", Year = 2019, transmission = Transmission.AUTOMATIC, engine = Engine.DIESEL },
            new { ID = "RO20 DFG", ownerID = "1004", Make = "Nissan", Model = "Qashqai", Year = 2020, transmission = Transmission.MANUAL, engine = Engine.HYBRID },
            new { ID = "YK14 PQR", ownerID = "1005", Make = "Toyota", Model = "Yaris", Year = 2014, transmission = Transmission.AUTOMATIC, engine = Engine.HYBRID },
            new { ID = "SN68 LMN", ownerID = "1006", Make = "BMW", Model = "1 Series", Year = 2018, transmission = Transmission.AUTOMATIC, engine = Engine.DIESEL },
            new { ID = "WV22 TUV", ownerID = "1007", Make = "Audi", Model = "A3", Year = 2022, transmission = Transmission.AUTOMATIC, engine = Engine.PETROL },
            new { ID = "FE16 GHK", ownerID = "1008", Make = "Mini", Model = "Hatch", Year = 2016, transmission = Transmission.MANUAL, engine = Engine.PETROL },
            new { ID = "CB67 WXY", ownerID = "1001", Make = "Mercedes-Benz", Model = "A-Class", Year = 2017, transmission = Transmission.AUTOMATIC, engine = Engine.DIESEL },
            new { ID = "MA21 ZZZ", ownerID = "1002", Make = "Kia", Model = "Puma", Year = 2021, transmission = Transmission.MANUAL, engine = Engine.ELECTRIC }
        );

        mb.Entity<Booking>().HasData(
            new { ID = "BK-1", bookedPlate = "LN15 XYA", Date = new DateTime(2026, 5, 10), Description = "Full service", TimeTaken = new TimeSpan(9, 0, 0) },
            new { ID = "BK-2", bookedPlate = "BD51 SMR", Date = new DateTime(2026, 5, 10), Description = "MOT and faults", TimeTaken = new TimeSpan(11, 30, 0) },
            new { ID = "BK-3", bookedPlate = "GL19 ABC", Date = new DateTime(2026, 5, 11), Description = "Battery check", TimeTaken = new TimeSpan(10, 0, 0) },
            new { ID = "BK-4", bookedPlate = "RO20 DFG", Date = new DateTime(2026, 5, 12), Description = "Brake inspection", TimeTaken = new TimeSpan(14, 0, 0) },
            new { ID = "BK-5", bookedPlate = "YK14 PQR", Date = new DateTime(2026, 5, 13), Description = "Engine rattling", TimeTaken = new TimeSpan(15, 0, 0) },
            new { ID = "BK-6", bookedPlate = "SN68 LMN", Date = new DateTime(2026, 5, 14), Description = "Wiper replacement", TimeTaken = new TimeSpan(16, 30, 0) }
        );

        mb.Entity<TrainingClass>().HasData(
            new { ID = "TC-1100", StudentID = "S001", Name = "Engine Basics", ClassType = ClassType.MAINTENANCE, Date = new DateTime(2026, 8, 1, 10, 0, 0), Description = "Learn engine basics" },
            new { ID = "TC-2100", StudentID = "S001", Name = "Advanced Engine Diagnostics", ClassType = ClassType.ADVANCED, Date = new DateTime(2026, 6, 15, 10, 0, 0), Description = "Learn how to diagnose ECU faults" },
            new { ID = "TC-1200", StudentID = "S002", Name = "Basic Car Maintenance", ClassType = ClassType.MAINTENANCE, Date = new DateTime(2026, 6, 20, 14, 0, 0), Description = "No description" }
        );

        mb.Entity<Repair>().HasData(
            new { ID = "R01", ascID = "BK-1", Description = "Oil and filter change", PartsCost = 53.00m, LabCost = 60.00m, Date = new DateTime(2026, 5, 10) },
            new { ID = "R02", ascID = "BK-1", Description = "Spark plug replacement", PartsCost = 50.00m, LabCost = 40.00m, Date = new DateTime(2026, 5, 10) },
            new { ID = "R03", ascID = "BK-2", Description = "Alternator fitting", PartsCost = 85.00m, LabCost = 120.00m, Date = new DateTime(2026, 5, 10) },
            new { ID = "R04", ascID = "BK-2", Description = "Diagnostic read", PartsCost = 0.00m, LabCost = 45.00m, Date = new DateTime(2026, 5, 10) },
            new { ID = "R05", ascID = "BK-3", Description = "Battery swap", PartsCost = 110.00m, LabCost = 30.00m, Date = new DateTime(2026, 5, 11) },
            new { ID = "R06", ascID = "BK-3", Description = "Battery terminal clean", PartsCost = 5.00m, LabCost = 20.00m, Date = new DateTime(2026, 5, 11) },
            new { ID = "R07", ascID = "BK-4", Description = "Front brake pads", PartsCost = 55.00m, LabCost = 70.00m, Date = new DateTime(2026, 5, 12) },
            new { ID = "R08", ascID = "BK-4", Description = "Front brake discs", PartsCost = 140.00m, LabCost = 80.00m, Date = new DateTime(2026, 5, 12) },
            new { ID = "R09", ascID = "BK-5", Description = "Engine flush", PartsCost = 35.00m, LabCost = 90.00m, Date = new DateTime(2026, 5, 13) },
            new { ID = "R10", ascID = "BK-5", Description = "Timing belt check", PartsCost = 0.00m, LabCost = 110.00m, Date = new DateTime(2026, 5, 13) },
            new { ID = "R11", ascID = "BK-6", Description = "Wipers replaced", PartsCost = 25.00m, LabCost = 15.00m, Date = new DateTime(2026, 5, 14) },
            new { ID = "R12", ascID = "BK-6", Description = "General wash and valet", PartsCost = 10.00m, LabCost = 20.00m, Date = new DateTime(2026, 5, 14) }
        );
        

    }
}