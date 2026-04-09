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
        Student studentA = new Student("S001", "Mek Student", "07562399573", "mek@example.com");
        Student studentB = new Student("S002", "Rob Student", "07511112222", "rob@example.com");
        Student studentC = new Student("S003", "Tom Student", "07533334444", "tom@example.com");
        Lecturer lecturerA = new Lecturer("L001", "Dr. Smith", "07555556666", "smithywithy@gmail.com");

        menu.staffList.Add(studentA);
        menu.staffList.Add(studentB);
        menu.staffList.Add(studentC);
        menu.staffList.Add(lecturerA);

        // Motorists
        Motorist m1 = new Motorist("M001", "Alice Driver", "alice@gmail.com", "07562389676");
        Motorist m2 = new Motorist("M002", "Ben Rider", "ben@hotmail.com", "07506289634");
        Motorist m3 = new Motorist("M003", "Cara Wheels", "cara@gmail.com", "07962365658");

        menu.motoristList.AddRange(new[] { m1, m2, m3 });

        // Vehicles
        Vehicle v1 = new Vehicle(m1, "AB12 CDE", "Toyota", "Yaris", "2018", Transmission.MANUAL, Engine.PETROL);
        Vehicle v2 = new Vehicle(m2, "XY99 ZZZ", "Ford", "Focus", "2016", Transmission.AUTOMATIC, Engine.DIESEL);

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
        Part p1 = new Part("Bosch", PartType.SUSPENSION, 45.50m, "Brake pads");
        Part p2 = new Part("Denso", PartType.ELECTRICAL, 120.00m, "Sensor");
        Part p3 = new Part("Castrol", PartType.ENGINE, 30.00m, "Oil Filter");

        menu.partsList.AddRange(new[] { p1, p2, p3 });

        Repair r1 = new Repair(
            b1,
            "Brake pads replacement",
            45.50m,
            80.00m,
            new List<Student> { studentA },
            new List<Part> { p1 },
            new DateTime(2026, 4, 10)
        );

        Repair r2 = new Repair(
            b2,
            "Sensor replacement and diagnostics",
            150.00m,
            110.00m,
            new List<Student> { studentA, studentB },
            new List<Part> { p2, p3 },
            new DateTime(2026, 4, 11)
        );
        
        Repair r3 = new Repair(
            b1,
            "Oil change",
            30.00m,
            50.00m,
            new List<Student> { studentC },
            new List<Part> { p3 },
            new DateTime(2026, 4, 10)
        );
        
        Repair r4 = new Repair(
            b2,
            "Oil change",
            30.00m,
            50.00m,
            new List<Student> { studentC },
            new List<Part> { p3 },
            new DateTime(2026, 4, 11)
        );
        

        b1.AddRepair(r1);
        b2.AddRepair(r2);
        b1.AddRepair(r3);

        menu.repairsList.AddRange(new[] { r1, r2, r3 });

        // Training classes
        TrainingClass c1 = new TrainingClass(
            studentA,
            "Advanced  Diagnostics",
            ClassType.ADVANCED,
            new DateTime(2026, 4, 15, 10, 0, 0),
            new List<Motorist> { m1, m2 },
            null
        );

        TrainingClass c2 = new TrainingClass(
            studentB,
            "Advanced Diagnostics",
            ClassType.ADVANCED,
            new DateTime(2026, 4, 18, 13, 30, 0),
            new List<Motorist> { m2, m3 },
            null
        );
        menu.classesList.AddRange(new[] { c1, c2 });

        Console.WriteLine("==================================");
        Console.WriteLine("Bookings");
        Console.WriteLine("==================================");
        foreach (Booking booking in menu.bookingsList)
        {
            Console.WriteLine(booking.ToString());
            
        }

        Console.WriteLine("==================================");
        Console.WriteLine("Training Class");
        Console.WriteLine("==================================");
        foreach (TrainingClass trainingClass in menu.classesList)
        {
            Console.WriteLine(trainingClass.ToString());
                foreach (Motorist attendee in trainingClass.attendees)
                {
                    Console.WriteLine($"  - {attendee.ID}: {attendee.Name} ({attendee.Email})");
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