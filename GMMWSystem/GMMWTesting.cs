namespace GMMWSystem;

/**
 * Test class to incorporate black box testing of basic console implementation
 */
public class GMMWTesting
{
    
    // Constructor to run tests
    public GMMWTesting(){}
    
    private List<Student> AddStudents() => new List<Student>
    {
        new Student("S001", "Oliver Smith", "07700900001", "o.smith@example.co.uk"),
        new Student("S002", "George Jones", "07700900002", "g.jones@example.co.uk"),
        new Student("S003", "Harry Taylor", "07700900003", "h.taylor@example.co.uk"),
        new Student("S004", "Jack Brown", "07700900004", "j.brown@example.co.uk"),
        new Student("S005", "Jacob Williams", "07700900005", "j.williams@example.co.uk"),
        new Student("S006", "Noah Davies", "07700900006", "n.davies@example.co.uk")
    };

    private List<Motorist> AddMotorists() => new List<Motorist>
    {
        new Motorist("M001", "Amelia Evans", "amelia.evans@gmail.com", "07700900011"),
        new Motorist("M002", "Olivia Wilson", "olivia.w@hotmail.co.uk", "07700900012"),
        new Motorist("M003", "Isla Thomas", "isla.thom@gmail.com", "07700900013"),
        new Motorist("M004", "Ava Roberts", "ava.r@yahoo.co.uk", "07700900014"),
        new Motorist("M005", "Emily Johnson", "emily.j@outlook.com", "07700900015"),
        new Motorist("M006", "Mia Wright", "mia.w@gmail.com", "07700900016"),
        new Motorist("M007", "Grace Robinson", "grace.rob@gmail.com", "07700900017"),
        new Motorist("M008", "Sophia Thompson", "sophia.t@yahoo.co.uk", "07700900018")
    };

    private List<Vehicle> AddVehicles(List<Motorist> m) => new List<Vehicle>
    {
        new Vehicle(m[0], "LN15 XYA", "Ford", "Fiesta", "2015", Transmission.MANUAL, Engine.PETROL),
        new Vehicle(m[1], "BD51 SMR", "Vauxhall", "Corsa", "2001", Transmission.MANUAL, Engine.PETROL),
        new Vehicle(m[2], "GL19 ABC", "Volkswagen", "Golf", "2019", Transmission.AUTOMATIC, Engine.DIESEL),
        new Vehicle(m[3], "RO20 DFG", "Nissan", "Qashqai", "2020", Transmission.MANUAL, Engine.HYBRID),
        new Vehicle(m[4], "YK14 PQR", "Toyota", "Yaris", "2014", Transmission.AUTOMATIC, Engine.HYBRID),
        new Vehicle(m[5], "SN68 LMN", "BMW", "1 Series", "2018", Transmission.AUTOMATIC, Engine.DIESEL),
        new Vehicle(m[6], "WV22 TUV", "Audi", "A3", "2022", Transmission.AUTOMATIC, Engine.PETROL),
        new Vehicle(m[7], "FE16 GHK", "Mini", "Hatch", "2016", Transmission.MANUAL, Engine.PETROL),
        new Vehicle(m[0], "CB67 WXY", "Mercedes-Benz", "A-Class", "2017", Transmission.AUTOMATIC, Engine.DIESEL),
        new Vehicle(m[1], "MA21 ZZZ", "Kia", "Puma", "2021", Transmission.MANUAL, Engine.ELECTRIC)
    };

    private List<Part> AddParts() => new List<Part>
    {
        new Part("Bosch", PartType.SUSPENSION, 55.00m, "Brake pads front"),
        new Part("Michelin", PartType.BODY, 25.00m, "Wiper blade set"),
        new Part("NGK", PartType.ENGINE, 12.50m, "Spark plug"),
        new Part("Castrol", PartType.ENGINE, 35.00m, "5W-30 Engine Oil 5L"),
        new Part("Bosch", PartType.ELECTRICAL, 85.00m, "Alternator"),
        new Part("Yuasa", PartType.ELECTRICAL, 110.00m, "12V Car Battery"),
        new Part("Brembo", PartType.SUSPENSION, 140.00m, "Brake disc pair"),
        new Part("Mann", PartType.ENGINE, 18.00m, "Air filter")
    };

    private List<Booking> AddBookings(List<Vehicle> v) => new List<Booking>
    {
        new Booking(v[0], new DateTime(2026, 5, 10)) { Description = "Full service", TimeTaken = new TimeSpan(9, 0, 0) },
        new Booking(v[1], new DateTime(2026, 5, 10)) { Description = "MOT and faults", TimeTaken = new TimeSpan(11, 30, 0) },
        new Booking(v[2], new DateTime(2026, 5, 11)) { Description = "Battery check", TimeTaken = new TimeSpan(10, 0, 0) },
        new Booking(v[3], new DateTime(2026, 5, 12)) { Description = "Brake inspection", TimeTaken = new TimeSpan(14, 0, 0) },
        new Booking(v[4], new DateTime(2026, 5, 13)) { Description = "Engine rattling", TimeTaken = new TimeSpan(15, 0, 0) },
        new Booking(v[5], new DateTime(2026, 5, 14)) { Description = "Wiper replacement", TimeTaken = new TimeSpan(16, 30, 0) }
    };

    private List<Repair> AddRepairs(List<Booking> b, List<Student> s, List<Part> p) => new List<Repair>
    {
        new Repair(b[0], "Oil and filter change", 53.00m, 60.00m, new List<Student> { s[0], s[1] }, new List<Part> { p[3], p[7] }, b[0].Date),
        new Repair(b[0], "Spark plug replacement", 50.00m, 40.00m, new List<Student> { s[2] }, new List<Part> { p[2] }, b[0].Date),
        
        new Repair(b[1], "Alternator fitting", 85.00m, 120.00m, new List<Student> { s[3], s[4] }, new List<Part> { p[4] }, b[1].Date),
        new Repair(b[1], "Diagnostic read", 0.00m, 45.00m, new List<Student> { s[5] }, new List<Part>(), b[1].Date),

        new Repair(b[2], "Battery swap", 110.00m, 30.00m, new List<Student> { s[0] }, new List<Part> { p[5] }, b[2].Date),
        new Repair(b[2], "Battery terminal clean", 5.00m, 20.00m, new List<Student> { s[1] }, new List<Part>(), b[2].Date),

        new Repair(b[3], "Front brake pads", 55.00m, 70.00m, new List<Student> { s[2], s[3] }, new List<Part> { p[0] }, b[3].Date),
        new Repair(b[3], "Front brake discs", 140.00m, 80.00m, new List<Student> { s[4] }, new List<Part> { p[6] }, b[3].Date),

        new Repair(b[4], "Engine flush", 35.00m, 90.00m, new List<Student> { s[5], s[0] }, new List<Part> { p[3] }, b[4].Date),
        new Repair(b[4], "Timing belt check", 0.00m, 110.00m, new List<Student> { s[1] }, new List<Part>(), b[4].Date),

        new Repair(b[5], "Wipers replaced", 25.00m, 15.00m, new List<Student> { s[2] }, new List<Part> { p[1] }, b[5].Date),
        new Repair(b[5], "General wash and valet", 10.00m, 20.00m, new List<Student> { s[3], s[4] }, new List<Part>(), b[5].Date),
    };

    private List<TrainingClass> AddTrainingClasses(List<Student> s, List<Motorist> m) => new List<TrainingClass>
    {
        new TrainingClass(s[0], "Engine Basics", ClassType.MAINTENANCE, new DateTime(2026, 8, 1, 10, 0, 0), new List<Motorist>(), "Learn engine basics"),
        new TrainingClass(s[0], "Advanced Engine Diagnostics", ClassType.ADVANCED, new DateTime(2026, 6, 15, 10, 0, 0), new List<Motorist> { m[0], m[1] }, "Learn how to diagnose ECU faults"),
        new TrainingClass(s[1], "Basic Car Maintenance", ClassType.MAINTENANCE, new DateTime(2026, 6, 20, 14, 0, 0), new List<Motorist> { m[3], m[4] }, null)
    };

    public void Test001()
    {
        // Register a vehicle owner's details
        List<Motorist> motorists = AddMotorists();
        Motorist motorist = motorists[0];
        
        bool passed = motorist.Name == "Amelia Evans" && motorist.Email == "amelia.evans@gmail.com" && motorist.PhoneNo == "07700900011";
        Console.WriteLine($"Test 001 - Register Motorist Details: {(passed ? "PASS" : "FAIL")}");
    }

    public void Test002()
    {
        // Record attendance and store class details
        List<Student> students = AddStudents();
        List<Motorist> motorists = AddMotorists();
        List<TrainingClass> classes = AddTrainingClasses(students, motorists);
        
        TrainingClass c = classes[0];
        Motorist m = motorists[2];
        
        c.register(m);
        
        bool passed = c.attendees.Contains(m) && c.Date == new DateTime(2026, 8, 1, 10, 0, 0) && c.Name == "Engine Basics";
        Console.WriteLine($"Test 002 - Record Class Attendance and Timetable: {(passed ? "PASS" : "FAIL")}");
    }

    public void Test003()
    {
        // Book vehicle for repair, associate with owner and faults
        List<Motorist> motorists = AddMotorists();
        List<Vehicle> vehicles = AddVehicles(motorists);
        List<Booking> bookings = AddBookings(vehicles);
        
        Booking b = bookings[0];
        
        bool passed = b.bookedVehicle == vehicles[0] && b.bookedVehicle.owner == motorists[0] && b.Description == "Full service";
        Console.WriteLine($"Test 003 - Book Vehicle and Associate Faults/Owner: {(passed ? "PASS" : "FAIL")}");
    }

    public void Test004()
    {
        // Record repair details, date, repairs carried out, parts, costs
        List<Motorist> motorists = AddMotorists();
        List<Vehicle> vehicles = AddVehicles(motorists);
        List<Booking> bookings = AddBookings(vehicles);
        List<Student> students = AddStudents();
        List<Part> parts = AddParts();
        List<Repair> repairs = AddRepairs(bookings, students, parts);
        
        Repair r = repairs[0]; 
        Part part1 = parts[3]; 
        Part part2 = parts[7]; 
        
        // Parts Cost = 35.00 + 18.00 = 53.00, Labour Cost = 60.00, Total = 113.00m
        bool detailsMatch = r.Date == bookings[0].Date && r.parts.Count == 2 && r.TotalCost == 113.00m && r.PartsCost == 53.00m && r.parts.Contains(part1) && r.parts.Contains(part2);
        Console.WriteLine($"Test 004 - Record Repair Details, Parts, and Costs: {(detailsMatch ? "PASS" : "FAIL")}");
    }

    public static void RunTest(String[] args)
    {
        // Instantiate test of workshop operations
        GMMWTesting test = new GMMWTesting();
        
        // Run test methods
        test.Test001();
        test.Test002();
        test.Test003();
        test.Test004();
    }
}