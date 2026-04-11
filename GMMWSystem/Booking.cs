using System.Text;

namespace GMMWSystem;

public class Booking
{
    public string ID { get; set; }
    public Vehicle bookedVehicle { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public TimeSpan? TimeTaken { get; set; }
    public List<Repair> Repairs { get; set; } = new();
}