namespace GMMWSystem;

public class Vehicle
{
    private string id;

    public string Id
    {
        get => id;
        set => id = value;
    }

    private Motorist owner;

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

    private string model;

    public string Model
    {
        get => model;
        set => model = value;
    }
    
    private Transmission transmission; 
    
    private Engine engine;
    
    private string year;
    
    public string Year
    {
        get => year;
        set => year = value;
    }

    public override string ToString()
    {
        return base.ToString();
    }
}