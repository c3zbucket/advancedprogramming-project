namespace GMMWSystem;

public class GMMWMenu
{
    public List<IStaff> staffList = new();
    public List<Motorist> motoristList = new();
    public List<Vehicle> vehicleList = new();
    public List<Booking> bookingsList = new();
    public List<Repair> repairsList = new();
    public List<Part> partsList = new();
    public List<TrainingClass> classesList = new();

    public GMMWMenu() { }

    public static void Main(string[] args)
    {
        GMMWMenu menu = new GMMWMenu();

        // Add staff
        Student studentA = new Student("S001", "Mek Student", "07562399573", "mek@example.com")
        {
            ID = "S001",
            Name = "Mek Student",
            PhoneNo = "07562399573",
            Email = "mek@example.com"
        };

        Student studentB = new Student("S002", "Rob Student", "07511112222", "rob@example.com")
        {
            ID = "S002",
            Name = "Rob Student",
            PhoneNo = "07511112222",
            Email = "rob@example.com"
        };

        Student studentC = new Student("S002", "Rob Student", "07511112222", "rob@example.com");

        menu.staffList.Add(studentA);
        menu.staffList.Add(studentB);

        // Motorists
        Motorist m1 = new Motorist("M001", "Alice Driver", "alice@drivers.com", "0700000001");
        Motorist m2 = new Motorist("M002", "Ben Rider", "ben@drivers.com", "0700000002");
        Motorist m3 = new Motorist("M003", "Cara Wheels", "cara@drivers.com", "0700000003");

        menu.motoristList.AddRange(new[] { m1, m2, m3 });

        // Vehicles
        Vehicle v1 = new Vehicle
        {
            Id = "V001",
            Plate = "AB12 CDE",
            Make = "Toyota",
            Model = "Yaris",
            Year = "2018",
            owner = m1
        };

        Vehicle v2 = new Vehicle
        {
            Id = "V002",
            Plate = "XY99 ZZZ",
            Make = "Ford",
            Model = "Focus",
            Year = "2016",
            owner = m2
        };

        menu.vehicleList.AddRange(new[] { v1, v2 });

        // Bookings
        Booking b1 = new Booking(v1, new DateTime(2026, 4, 10))
        {
            Description = "General service and brake check",
            Time = new TimeSpan(9, 30, 0)
        };

        Booking b2 = new Booking(v2, new DateTime(2026, 4, 11))
        {
            Description = "Engine warning light diagnosis",
            Time = new TimeSpan(14, 0, 0)
        };

        menu.bookingsList.AddRange(new[] { b1, b2 });

        // Parts
        Part p1 = new Part { Cost = 45.50m };
        Part p2 = new Part { Cost = 120.00m };
        Part p3 = new Part { Cost = 30.00m };

        menu.partsList.AddRange(new[] { p1, p2, p3 });

        Repair r1 = new Repair(
            b1,
            "Brake pads replacement",
            partsCost: 45.50m,
            labCost: 80.00m,
            repairers: new List<Student> { studentA },
            parts: new List<Part> { p1 }
        );

        Repair r2 = new Repair(
            b2,
            "Sensor replacement and diagnostics",
            partsCost: 150.00m,
            labCost: 110.00m,
            repairers: new List<Student> { studentA, studentB },
            parts: new List<Part> { p2, p3 }
        );
        
        Repair r3 = new Repair(
            b1,
            "Oil change",
            partsCost: 30.00m,
            labCost: 50.00m,
            repairers: new List<Student> { studentC },
            parts: new List<Part> { p3 }
        );
        
        Repair r4 = new Repair(
            b2,
            "Oil change",
            partsCost: 30.00m,
            labCost: 50.00m,
            repairers: new List<Student> { studentC },
            parts: new List<Part> { p3 }
        );
        

        b1.AddRepair(r1);
        b2.AddRepair(r2);

        menu.repairsList.AddRange(new[] { r1, r2 });

        // Training classes
        TrainingClass c1 = new TrainingClass(
            "C001",
            studentA,
            "Basic Maintenance",
            new DateTime(2026, 4, 15, 10, 0, 0),
            new List<Motorist> { m1, m2 }
        )
        {
            Name = "Basic Maintenance",
            ClassType = ClassType.MAINTENANCE,
            Description = "Oil checks, tyre pressure, basic safety",
            Date = new DateTime(2026, 4, 15, 10, 0, 0),
            attendees = new List<Motorist> { m1, m2 }
        };

        TrainingClass c2 = new TrainingClass(
            "C002",
            studentB,
            "Advanced Diagnostics",
            new DateTime(2026, 4, 18, 13, 30, 0),
            new List<Motorist> { m2, m3 }
        )
        {
            Name = "Advanced Diagnostics",
            ClassType = ClassType.ADVANCED,
            Description = "Dashboard warnings and fault-finding workflow",
            Date = new DateTime(2026, 4, 18, 13, 30, 0),
            attendees = new List<Motorist> { m2, m3 }
        };

        menu.classesList.AddRange(new[] { c1, c2 });

        // Display test output
        Console.WriteLine("==================================");
        Console.WriteLine("Bookings");
        Console.WriteLine("==================================");
        foreach (Booking booking in menu.bookingsList)
        {
            Console.WriteLine($"Booking ID: {booking.Id}");
            Console.WriteLine($"Date/Time : {booking.Date:yyyy-MM-dd} {booking.Time}");
            Console.WriteLine(
                $"Vehicle   : {booking.bookedVehicle.Make} {booking.bookedVehicle.Model} ({booking.bookedVehicle.Plate})");
            Console.WriteLine(
                $"Owner     : {booking.bookedVehicle.owner.Name} | {booking.bookedVehicle.owner.PhoneNo}");
            Console.WriteLine($"Desc      : {booking.Description}");
            Console.WriteLine($"Repairs   : {booking.Repairs.Count}");
            Console.WriteLine();
        }

        Console.WriteLine("==================================");
        Console.WriteLine("Training Class");
        Console.WriteLine("==================================");
        foreach (TrainingClass trainingClass in menu.classesList)
        {
            Console.WriteLine($"Class ID   : {trainingClass.ID}");
            Console.WriteLine($"Name       : {trainingClass.Name}");
            Console.WriteLine($"Type       : {trainingClass.ClassType}");
            Console.WriteLine($"Date       : {trainingClass.Date:yyyy-MM-dd HH:mm}");
            Console.WriteLine($"Description: {trainingClass.Description}");
            Console.WriteLine($"Attendees  : {trainingClass.Count()}");

            if (trainingClass.attendees != null)
            {
                foreach (Motorist attendee in trainingClass.attendees)
                {
                    Console.WriteLine($"  - {attendee.ID}: {attendee.Name} ({attendee.Email})");
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("==================================");
        Console.WriteLine("Repairs");
        Console.WriteLine("==================================");
        int i = 1;
        foreach (Repair repair in menu.repairsList)
        {
            // Repair currently only exposes a generic ToString(), so include related counts too.
            Console.WriteLine($"Repair #{i}: {repair}");
            Console.WriteLine($"  Repairers: {repair.repairers.Count}");
            Console.WriteLine($"  Parts    : {repair.parts.Count}");
            Console.WriteLine();
            i++;
        }
    }
}