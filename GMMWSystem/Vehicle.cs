namespace GMMWSystem;
using System.Text;

public class Vehicle
{
    public string ID { get; set; }
    public Motorist owner { get; set; }

    public string Plate { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public Transmission transmission { get; set; } 
    public Engine engine { get; set; }
    public string Year { get; set; }

    public Vehicle()
    {
        owner = new Motorist("M-UNKNOWN", "Unknown", "unknown@example.com", string.Empty);
        transmission = Transmission.MANUAL;
        engine = Engine.PETROL;
        ID = IDGen(this);
    }

    public Vehicle(Motorist owner, string plate, string make, string model, string year,
        Transmission transmission = Transmission.MANUAL, Engine engine = Engine.PETROL)
    {
        this.owner = owner;
        this.Plate = plate;
        this.Make = make;
        this.Model = model;
        this.Year = year;
        this.transmission = transmission;
        this.engine = engine;
        ID = IDGen(this);
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