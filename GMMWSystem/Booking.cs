using System.Security.Principal;
using System.Text;

namespace GMMWSystem;

public class Booking : Record<Vehicle, DateTime>
{
    private string id;
    public override string ID { get; }
    public Vehicle bookedVehicle { get; set; }

    public string Description { get; set; }
    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public List<Repair> Repairs { get; set; } = new();
    
    /**
     * Generate a unique ID based on the registered vehicle's plate and time
     */
    public override string IDGen(Vehicle vehicle, DateTime date)
    {
        
        StringBuilder id = new();
        id.AppendFormat($"BK-{vehicle.Plate}{date}");
        return id.ToString();
    }
    
    public Booking(Vehicle vehicle, DateTime date)
    {
        id = IDGen(vehicle,date);
        bookedVehicle = vehicle;
        Date = date;
    }

    public void Add(Repair repair)
    {
        Repairs.Add(repair);
        UpdateTotals();
    }

    public void RemoveRepair(Repair repair)
    {
        if (Repairs.Contains(repair))
        {
            Repairs.Remove(repair);
            UpdateTotals();
        }
    }

    public void AddRepair(Repair repair)
    {
        Add(repair);
    }
    /**
     * Get details of the booked vehicle's owner
     */
    public Motorist getDetails() => bookedVehicle.owner;
    
    private void UpdateTotals()
    {
        //TotalCost = TotalLab + TotalParts;
    }

    public override string ToString()
    {
        return $"Booking[{ID}] {Date:yyyy-MM-dd} {Time:hh\\:mm} Vehicle={bookedVehicle.Make} {bookedVehicle.Model} ({bookedVehicle.Plate}) Owner={bookedVehicle.owner.Name} Repairs={Repairs.Count} Desc={Description}";
    }
}