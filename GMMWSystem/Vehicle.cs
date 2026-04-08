namespace GMMWSystem;

public class Vehicle
{
    private string id;

    private Visitor owner;

    private string plate;
    
    private string make;
    
    private string model;
    
    private Transmission transmission; 
    
    private Engine engine;
    
    private string year;

    public override string ToString()
    {
        return base.ToString();
    }
}