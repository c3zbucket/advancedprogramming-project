using System.Security.Principal;
using System.Text;

namespace GMMWSystem;

public class Booking
{
    public string Id { get; private set; } 
    public Vehicle bookedVehicle { get; set; }

    public string Description { get; set; }
    public DateTime Date { get; set; }

    public TimeSpan Time { get; set; }

    public List<Repair> Repairs { get; set; } = new();
    
    public Booking(Vehicle vehicle, DateTime date)
    {
        Id = idGen(vehicle,date);
        bookedVehicle = vehicle;
        Date = date;
    }

    /**
     * Generate a unique ID based on the registered vehicle's plate and time
     */
    public string idGen(Vehicle vehicle, DateTime date)
    {
        StringBuilder id = new();
        id.AppendFormat($"ID-, {vehicle.Plate}, {date}");
        return id.ToString();
    }
    

    public void AddRepair(Repair repair)
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
        //return $"Booking {Id}: {BookedVehicle.Model} for {Owner.Name} - Total: £{TotalCost}";
        return "Booking";
    }
       
}