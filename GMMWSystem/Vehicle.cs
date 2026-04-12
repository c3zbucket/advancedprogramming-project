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
}