namespace GMMWSystem;
using System.Text;

public class Vehicle : Record<Vehicle, Motorist>
{
    private string id;

    public override string ID { get => id; }
    public Motorist owner;

    private string plate;

    public string Plate
    {
        get => plate;
        set => plate = value;
    }
    
    private string make;

    public string Make
    {
        get => make;
        set => make = value;
    }

    private string model = string.Empty;

    public string Model
    {
        get => model;
        set => model = value;
    }
    
    private Transmission transmission; 
    
    private Engine engine;
    
    private string year = string.Empty;
    
    public string Year
    {
        get => year;
        set => year = value;
    }

    public Vehicle()
    {
        owner = new Motorist("M-UNKNOWN", "Unknown", "unknown@example.com", string.Empty);
        transmission = Transmission.MANUAL;
        engine = Engine.PETROL;
        id = IDGen(this, owner);
    }

    public Vehicle(Motorist owner, string plate, string make, string model, string year,
        Transmission transmission = Transmission.MANUAL, Engine engine = Engine.PETROL)
    {
        this.owner = owner;
        this.plate = plate;
        this.make = make;
        this.model = model;
        this.year = year;
        this.transmission = transmission;
        this.engine = engine;
        id = IDGen(this, owner);
    }

    public override string IDGen(Vehicle vehicle, Motorist m)
    {
        return IDGen(vehicle);
    }
    
    
    public string IDGen(Vehicle vehicle)
    {
        StringBuilder id = new();
        id.AppendFormat($"V-{vehicle.Plate.Replace(" ", string.Empty).ToUpperInvariant()}");
        return id.ToString();
    }

    public override string ToString() 
        => $"Vehicle[{ID}] {Make} {Model} ({Year}) Plate={Plate} Owner={owner.Name} Transmission={transmission} Engine={engine}";
    
}