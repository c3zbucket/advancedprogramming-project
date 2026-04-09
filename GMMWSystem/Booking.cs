using System.Globalization;
using System.Security.Principal;
using System.Text;

namespace GMMWSystem;

public class Booking : Record<Vehicle, DateTime>
{
    public override string ID { get; }
    public Vehicle bookedVehicle { get; }

    public string Description { get; set; }
    public DateTime Date { get; set; }

    public TimeSpan TimeTaken { get; set; }

    public List<Repair> Repairs { get; } = new();
    
    /**
     * Generate a unique ID based on the registered vehicle's plate and time
     */
    public override string IDGen(Vehicle vehicle, DateTime date)
    {
        
        StringBuilder id = new();
        id.AppendFormat($"BK-{vehicle.Plate.Remove(5,1)}-{date:ddMM}");
        return id.ToString();
    }
    
    public Booking(Vehicle vehicle, DateTime date)
    {
        ID = IDGen(vehicle,date);
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

    public string LinkedRepairs()
    {
        if (Repairs.Count == 0) return "No repairs linked to this booking";
        StringBuilder repairList = new();
        Repairs.ForEach(repair => repairList.AppendLine(repair.ToString()));
        return repairList.ToString();
    }

    public override string ToString()
    {
        return $"[Booking ID: [{ID}] \n Date: [{Date:dd/MM/yyyy} {TimeTaken:hh\\:mm}] \n Plate: {bookedVehicle.Plate.Trim()} \n Vehicle: {bookedVehicle.Make} {bookedVehicle.Model} \n Owner: {bookedVehicle.owner.Name} \n  Description: {Description} \n Repairs done: {Repairs.Count} | Details \n [ {LinkedRepairs()} ]";
    }
}