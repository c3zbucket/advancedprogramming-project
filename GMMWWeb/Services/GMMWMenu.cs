using Microsoft.EntityFrameworkCore;

namespace GMMWWeb.Services;

/*
public class GMMWMenu
{
    public static void Menu()
    {
        Console.WriteLine("\n---Welcome to Greater Manchester Motor Workshop---");
        Console.WriteLine("\n---Enter the number for the option---");
        Console.WriteLine("1. Booking Management");
        Console.WriteLine("2. Repair Management");
        Console.WriteLine("3. Motorist Management");
        Console.WriteLine("4. Vehicle Management");
        Console.WriteLine("5. Parts Management");
        Console.WriteLine("6. Training Class Management");
        Console.WriteLine("7. Staff Management");
        Console.WriteLine("8. Debug");
        Console.WriteLine("0. Exit");
    }

    public static int? userPrompt()
    {
        Console.Write("Choice: ");
        var valid = int.TryParse(Console.ReadLine(), out int choice);
        Console.WriteLine();
        return valid ? choice : null;
    }

    public static void Main(string[] args)
    {
        bool exit = false;
        int? choice;
        do
        {
            Menu();
            choice = userPrompt();
            switch (choice)
            {
                case 1:
                    BookingMenu();
                    break;
                case 2:
                    RepairsMenu();
                    break;
                case 3:
                    MotoristMenu();
                    break;
                case 4:
                    VehicleMenu();
                    break;
                case 5:
                    PartsMenu();
                    break;
                case 6:
                    TCMenu();
                    break;
                case 7:
                    UserMenu();
                    break;
                case 8:
                    DebugMenu();
                    break;
                case 0:
                    exit = true;
                    Console.WriteLine("Exiting program...");
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        } while (!exit);
    }

    private static void MotoristMenu()
    {
        using var context = new GMMWDBContext();
        var motorists = context.Motorists.ToList();
        Console.WriteLine("\n--- Registered Motorists ---");
        foreach (var m in motorists)
        {
            Console.WriteLine($"ID: {m.ID} | Name: {m.Name} | Phone: {m.PhoneNo} | Email: {m.Email}");
        }
    }

    private static void VehicleMenu()
    {
        using var context = new GMMWDBContext();
        var vehicles = context.Vehicles.Include(v => v.owner).ToList();
        Console.WriteLine("\n--- Registered Vehicles ---");
        foreach (var v in vehicles)
        {
            Console.WriteLine($"Vehicle[{v.ID}] {v.Make} {v.Model} ({v.Year}) Plate={v.Plate} Owner={v.owner?.Name} Transmission={v.transmission} Engine={v.engine}");
        }
    }

    private static void BookingMenu()
    {
        using var context = new GMMWDBContext();
        var bookings = context.Bookings
            .Include(b => b.bookedVehicle)
            .ThenInclude(v => v.owner)
            .Include(b => b.Repairs)
            .ToList();
            
        Console.WriteLine("\n--- Registered Bookings ---");
        foreach (var b in bookings)
        {
            Console.WriteLine($"[Booking ID: {b.ID}]\n Date: {b.Date:dd/MM/yyyy} {b.TimeTaken:hh\\:mm}\n Vehicle: {b.bookedVehicle.Make} {b.bookedVehicle.Model} ({b.bookedVehicle.Plate})\n Owner: {b.bookedVehicle.owner.Name}\n Description: {b.Description}\n Repairs linked: {b.Repairs.Count}");
        }
    }

    private static void RepairsMenu()
    {
        using var context = new GMMWDBContext();
        var repairs = context.Repairs
            .Include(r => r.ascBooking)
            .Include(r => r.parts)
            .Include(r => r.repairers)
            .ToList();
            
        Console.WriteLine("\n--- Registered Repairs ---");
        foreach (var r in repairs)
        {
            Console.WriteLine($"[Repair ID: {r.ID}] Booking: {r.ascBooking.ID} | Date: {r.Date:dd/MM/yyyy HH:mm} | Desc: {r.Description} | Cost: {r.TotalCost:C} (Parts {r.PartsCost:C}, Labour {r.LabCost:C}) | Repairers: {r.repairers?.Count ?? 0} | Parts: {r.parts?.Count ?? 0}");
        }
    }

    private static void PartsMenu()
    {
        using var context = new GMMWDBContext();
        var parts = context.Parts.ToList();
        Console.WriteLine("\n--- Registered Parts ---");
        foreach (var p in parts)
        {
            Console.WriteLine($"Part[{p.ID}] | Make: {p.Make} | Type: {p.Type} | Cost: {p.Cost:C} | Description: {p.Description}");
        }
    }

    private static void TCMenu()
    {
        using var context = new GMMWDBContext();
        var classes = context.TrainingClass
            .Include(tc => tc.student)
            .Include(tc => tc.attendees)
            .ToList();
            
        Console.WriteLine("\n--- Training Classes ---");
        foreach (var c in classes)
        {
            Console.WriteLine($"[Class ID: {c.ID}] Name: {c.Name} | Type: {c.ClassType} | Date: {c.Date:dd/MM/yyyy HH:mm} | Taught by: {c.student?.Name} | Attendees: {c.attendees?.Count ?? 0} | Desc: {c.Description}");
        }
    }
    
    private static void UserMenu()
    {
        using var context = new GMMWDBContext();
        var staffList = context.Staff.ToList();
        Console.WriteLine("\n--- Staff ---");
        foreach (var s in staffList)
        {
            Console.WriteLine($"Staff ID: {s.ID} | Name: {s.Name} | Phone: {s.PhoneNo} | Email: {s.Email}");
        }
        Console.WriteLine("\n--- System Users ---");
        var userList = context.Users.ToList();
        foreach (var u in userList)
        {
            Console.WriteLine($"Linked Staff ID: {u.ID} | Linked Staff Name: {u.Member.Name} | System Role : {u.Role} | Phone No.: {u.Member.PhoneNo} | Email: {u.Member.Email}");
        }
    }
    
    private static void DebugMenu()
    {
        using var context = new GMMWDBContext();
        Console.WriteLine("\n--- Debug Menu ---");
        Console.WriteLine("\n Wiping and recreating schema...");
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        Console.WriteLine("\n Adding debug entities...");
        var students = new List<Student>
        { new Student { ID = "S001", Name = "Oliver Smith", PhoneNo = "07700900001", Email = "o.smith@outlook.com" },
            new Student { ID = "S002", Name = "George Jones", PhoneNo = "07700900002", Email = "g.jones@gmail.com" },
            new Student { ID = "S003", Name = "Harry Taylor", PhoneNo = "07700900003", Email = "h.taylor@outlook.com" },
            new Student { ID = "S004", Name = "Jack Brown", PhoneNo = "07700900004", Email = "j.brown@gmail.com" },
            new Student { ID = "S005", Name = "Jacob Williams", PhoneNo = "07700900005", Email = "j.williams@outlook.com" },
            new Student { ID = "S006", Name = "Noah Davies", PhoneNo = "07700900006", Email = "n.davies@gmail.com" }
        };
        context.AddRange(students);
        context.SaveChanges();

        var lecturers = new List<Lecturer>
        {
            new Lecturer { ID = "L001", Name = "Alice Johnson", PhoneNo = "07700900101", Email = "a.johnson@gmail.com" },
            new Lecturer { ID = "L002", Name = "Rob Youblind", PhoneNo = "07700900102", Email = "r.youblind@outlook.com" }
        };
        context.AddRange(lecturers);
        context.SaveChanges();

        var admins = new Admin { ID = "A001", Name = "Greg Adminny", PhoneNo = "07700900999", Email = "greg.a@outlook.com" };
        context.AddRange(admins);
        context.SaveChanges();

        var users = new List<SystemUser>
        {
            new SystemUser { ID = lecturers[0].ID, Member = lecturers[0], Password = "password123", Role = Role.LECTURER },
            new SystemUser { ID = lecturers[1].ID, Member = lecturers[1], Password = "password123", Role = Role.LECTURER },
            new SystemUser {ID = students[0].ID, Member = students[0], Password = "admin123", Role = Role.ADMIN },
            new SystemUser {ID = students[1].ID, Member = students[1], Password = "password123", Role = Role.STUDENT },
            new SystemUser {ID = students[2].ID, Member = students[2], Password = "password123", Role = Role.STUDENT },
            new SystemUser {ID = students[3].ID, Member = students[3], Password = "password123", Role = Role.STUDENT },
            new SystemUser {ID = admins.ID, Member = admins, Password = "admin", Role = Role.ADMIN }
        };
        context.Users.AddRange(users);
        context.SaveChanges();

        var motorists = new List<Motorist>
        {
            new Motorist { ID = "M001", Name = "Amelia Evans", Email = "amelia.evans@gmail.com", PhoneNo = "07700900011" },
            new Motorist { ID = "M002", Name = "Olivia Wilson", Email = "olivia.w@hotmail.co.uk", PhoneNo = "07700900012" },
            new Motorist { ID = "M003", Name = "Isla Thomas", Email = "isla.thom@gmail.com", PhoneNo = "07700900013" },
            new Motorist { ID = "M004", Name = "Ava Roberts", Email = "ava.r@yahoo.co.uk", PhoneNo = "07700900014" },
            new Motorist { ID = "M005", Name = "Emily Johnson", Email = "emily.j@outlook.com", PhoneNo = "07700900015" },
            new Motorist { ID = "M006", Name = "Mia Wright", Email = "mia.w@gmail.com", PhoneNo = "07700900016" },
            new Motorist { ID = "M007", Name = "Grace Robinson", Email = "grace.rob@gmail.com", PhoneNo = "07700900017" },
            new Motorist { ID = "M008", Name = "Sophia Thompson", Email = "sophia.t@yahoo.co.uk", PhoneNo = "07700900018" }
        };
        context.Motorists.AddRange(motorists);
        context.SaveChanges();

        var vehicles = new List<Vehicle>
        {
            new Vehicle { ID = "V-01", owner = motorists[0], Plate = "LN15 XYA", Make = "Ford", Model = "Fiesta", Year = "2015", transmission = Transmission.MANUAL, engine = Engine.PETROL },
            new Vehicle { ID = "V-02", owner = motorists[1], Plate = "BD51 SMR", Make = "Vauxhall", Model = "Corsa", Year = "2001", transmission = Transmission.MANUAL, engine = Engine.PETROL },
            new Vehicle { ID = "V-03", owner = motorists[2], Plate = "GL19 ABC", Make = "Volkswagen", Model = "Golf", Year = "2019", transmission = Transmission.AUTOMATIC, engine = Engine.DIESEL },
            new Vehicle { ID = "V-04", owner = motorists[3], Plate = "RO20 DFG", Make = "Nissan", Model = "Qashqai", Year = "2020", transmission = Transmission.MANUAL, engine = Engine.HYBRID },
            new Vehicle { ID = "V-05", owner = motorists[4], Plate = "YK14 PQR", Make = "Toyota", Model = "Yaris", Year = "2014", transmission = Transmission.AUTOMATIC, engine = Engine.HYBRID },
            new Vehicle { ID = "V-06", owner = motorists[5], Plate = "SN68 LMN", Make = "BMW", Model = "1 Series", Year = "2018", transmission = Transmission.AUTOMATIC, engine = Engine.DIESEL },
            new Vehicle { ID = "V-07", owner = motorists[6], Plate = "WV22 TUV", Make = "Audi", Model = "A3", Year = "2022", transmission = Transmission.AUTOMATIC, engine = Engine.PETROL },
            new Vehicle { ID = "V-08", owner = motorists[7], Plate = "FE16 GHK", Make = "Mini", Model = "Hatch", Year = "2016", transmission = Transmission.MANUAL, engine = Engine.PETROL },
            new Vehicle { ID = "V-09", owner = motorists[0], Plate = "CB67 WXY", Make = "Mercedes-Benz", Model = "A-Class", Year = "2017", transmission = Transmission.AUTOMATIC, engine = Engine.DIESEL },
            new Vehicle { ID = "V-010", owner = motorists[1], Plate = "MA21 ZZZ", Make = "Kia", Model = "Puma", Year = "2021", transmission = Transmission.MANUAL, engine = Engine.ELECTRIC }
        };
        context.Vehicles.AddRange(vehicles);
        context.SaveChanges();

        var parts = new List<Part>
        {
            new Part { ID = "P-BOSCH3", Make = "Bosch", Type = PartType.SUSPENSION, Cost = 55.00m, Description = "Brake pads front" },
            new Part { ID = "P-MICHELIN5", Make = "Michelin", Type = PartType.BODY, Cost = 25.00m, Description = "Wiper blade set" },
            new Part { ID = "P-NGK1", Make = "NGK", Type = PartType.ENGINE, Cost = 12.50m, Description = "Spark plug" },
            new Part { ID = "P-CASTROL1", Make = "Castrol", Type = PartType.ENGINE, Cost = 35.00m, Description = "5W-30 Engine Oil 5L" },
            new Part { ID = "P-BOSCH2", Make = "Bosch", Type = PartType.ELECTRICAL, Cost = 85.00m, Description = "Alternator" },
            new Part { ID = "P-YUASA2", Make = "Yuasa", Type = PartType.ELECTRICAL, Cost = 110.00m, Description = "12V Car Battery" },
            new Part { ID = "P-BREMBO3", Make = "Brembo", Type = PartType.SUSPENSION, Cost = 140.00m, Description = "Brake disc pair" },
            new Part { ID = "P-MANN1", Make = "Mann", Type = PartType.ENGINE, Cost = 18.00m, Description = "Air filter" }
        };
        context.Parts.AddRange(parts);
        context.SaveChanges();

        var bookings = new List<Booking>
        {
            new Booking { ID = "BK-1", bookedVehicle = vehicles[0], Date = new DateTime(2026, 5, 10), Description = "Full service", TimeTaken = new TimeSpan(9, 0, 0), Repairs = new List<Repair>() },
            new Booking { ID = "BK-2", bookedVehicle = vehicles[1], Date = new DateTime(2026, 5, 10), Description = "MOT and faults", TimeTaken = new TimeSpan(11, 30, 0), Repairs = new List<Repair>() },
            new Booking { ID = "BK-3", bookedVehicle = vehicles[2], Date = new DateTime(2026, 5, 11), Description = "Battery check", TimeTaken = new TimeSpan(10, 0, 0), Repairs = new List<Repair>() },
            new Booking { ID = "BK-4", bookedVehicle = vehicles[3], Date = new DateTime(2026, 5, 12), Description = "Brake inspection", TimeTaken = new TimeSpan(14, 0, 0), Repairs = new List<Repair>() },
            new Booking { ID = "BK-5", bookedVehicle = vehicles[4], Date = new DateTime(2026, 5, 13), Description = "Engine rattling", TimeTaken = new TimeSpan(15, 0, 0), Repairs = new List<Repair>() },
            new Booking { ID = "BK-6", bookedVehicle = vehicles[5], Date = new DateTime(2026, 5, 14), Description = "Wiper replacement", TimeTaken = new TimeSpan(16, 30, 0), Repairs = new List<Repair>() }
        };
        context.Bookings.AddRange(bookings);
        context.SaveChanges();

        var repairs = new List<Repair>
        {
            new Repair { ID = "R01", ascBooking = bookings[0], Description = "Oil and filter change", PartsCost = 53.00m, LabCost = 60.00m, repairers = new List<Student> { students[0], students[1] }, parts = new List<Part> { parts[3], parts[7] }, Date = bookings[0].Date },
            new Repair { ID = "R02", ascBooking = bookings[0], Description = "Spark plug replacement", PartsCost = 50.00m, LabCost = 40.00m, repairers = new List<Student> { students[2] }, parts = new List<Part> { parts[2] }, Date = bookings[0].Date },
            new Repair { ID = "R03", ascBooking = bookings[1], Description = "Alternator fitting", PartsCost = 85.00m, LabCost = 120.00m, repairers = new List<Student> { students[3], students[4] }, parts = new List<Part> { parts[4] }, Date = bookings[1].Date },
            new Repair { ID = "R04", ascBooking = bookings[1], Description = "Diagnostic read", PartsCost = 0.00m, LabCost = 45.00m, repairers = new List<Student> { students[5] }, parts = new List<Part>(), Date = bookings[1].Date },
            new Repair { ID = "R05", ascBooking = bookings[2], Description = "Battery swap", PartsCost = 110.00m, LabCost = 30.00m, repairers = new List<Student> { students[0] }, parts = new List<Part> { parts[5] }, Date = bookings[2].Date },
            new Repair { ID = "R06", ascBooking = bookings[2], Description = "Battery terminal clean", PartsCost = 5.00m, LabCost = 20.00m, repairers = new List<Student> { students[1] }, parts = new List<Part>(), Date = bookings[2].Date },
            new Repair { ID = "R07", ascBooking = bookings[3], Description = "Front brake pads", PartsCost = 55.00m, LabCost = 70.00m, repairers = new List<Student> { students[2], students[3] }, parts = new List<Part> { parts[0] }, Date = bookings[3].Date },
            new Repair { ID = "R08", ascBooking = bookings[3], Description = "Front brake discs", PartsCost = 140.00m, LabCost = 80.00m, repairers = new List<Student> { students[4] }, parts = new List<Part> { parts[6] }, Date = bookings[3].Date },
            new Repair { ID = "R09", ascBooking = bookings[4], Description = "Engine flush", PartsCost = 35.00m, LabCost = 90.00m, repairers = new List<Student> { students[5], students[0] }, parts = new List<Part> { parts[3] }, Date = bookings[4].Date },
            new Repair { ID = "R10", ascBooking = bookings[4], Description = "Timing belt check", PartsCost = 0.00m, LabCost = 110.00m, repairers = new List<Student> { students[1] }, parts = new List<Part>(), Date = bookings[4].Date },
            new Repair { ID = "R11", ascBooking = bookings[5], Description = "Wipers replaced", PartsCost = 25.00m, LabCost = 15.00m, repairers = new List<Student> { students[2] }, parts = new List<Part> { parts[1] }, Date = bookings[5].Date },
            new Repair { ID = "R12", ascBooking = bookings[5], Description = "General wash and valet", PartsCost = 10.00m, LabCost = 20.00m, repairers = new List<Student> { students[3], students[4] }, parts = new List<Part>(), Date = bookings[5].Date }
        };
        bookings[0].Repairs.AddRange(new[] { repairs[0], repairs[1] });
        bookings[1].Repairs.AddRange(new[] { repairs[2], repairs[3] });
        bookings[2].Repairs.AddRange(new[] { repairs[4], repairs[5] });
        bookings[3].Repairs.AddRange(new[] { repairs[6], repairs[7] });
        bookings[4].Repairs.AddRange(new[] { repairs[8], repairs[9] });
        bookings[5].Repairs.AddRange(new[] { repairs[10], repairs[11] });
        context.Repairs.AddRange(repairs);
        context.SaveChanges();

        var trainingClasses = new List<TrainingClass>
        {
            new TrainingClass { ID = "TC-1100", student = students[0], Name = "Engine Basics", ClassType = ClassType.MAINTENANCE, Date = new DateTime(2026, 8, 1, 10, 0, 0), attendees = new List<Motorist>(), Description = "Learn engine basics" },
            new TrainingClass { ID = "TC-2100", student = students[0], Name = "Advanced Engine Diagnostics", ClassType = ClassType.ADVANCED, Date = new DateTime(2026, 6, 15, 10, 0, 0), attendees = new List<Motorist> { motorists[0], motorists[1] }, Description = "Learn how to diagnose ECU faults" },
            new TrainingClass { ID = "TC-1200", student = students[1], Name = "Basic Car Maintenance", ClassType = ClassType.MAINTENANCE, Date = new DateTime(2026, 6, 20, 14, 0, 0), attendees = new List<Motorist> { motorists[3], motorists[4] }, Description = "No description" }
        };
        context.TrainingClass.AddRange(trainingClasses);
        context.SaveChanges();

        Console.WriteLine("Database populated with debug entities");
    }
}
*/