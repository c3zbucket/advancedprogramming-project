namespace GMMWSystem;

public class Booking
{
    public string Id { get; private set; }
    public Vehicle BookedVehicle { get; set; }
    
    public Visitor Owner { get; set; }

    public string Description { get; set; }
    public DateTime Date { get; set; }
    
    public TimeSpan Time { get; set; }

    public List<Repair> Repairs { get; set; } = new();


    public Booking(string id, Visitor owner, Vehicle vehicle)
    {
        Id = id;
        Owner = owner;
        BookedVehicle = vehicle;
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

    private void UpdateTotals()
    {
        //TotalCost = TotalLab + TotalParts;
    }

    public override string ToString()
    {
        //return $"Booking {Id}: {BookedVehicle.Model} for {Owner.Name} - Total: £{TotalCost}";
    }
}