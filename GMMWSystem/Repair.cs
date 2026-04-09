using System.Text;

namespace GMMWSystem;

public class Repair : Record<Booking,DateTime>
{
    public override string ID { get; }

    public Booking ascBooking { get; private set; }
    public string Description { get; private set; }
    public List<Student> repairers { get; private set; }
    public decimal PartsCost { get; private set; }
    public List<Part> parts { get; private set; }
    public DateTime Date { get; private set; }
    public decimal LabCost { get; protected set; }
    public decimal TotalCost => PartsCost + LabCost;
    
    public Repair(Booking ascBooking, string description, decimal partsCost, decimal labCost, IEnumerable<Student>repairers, IEnumerable<Part>parts, DateTime date)
    {
        this.ascBooking = ascBooking;
        Description = description;
        PartsCost = partsCost;
        LabCost = labCost;
        this.repairers = repairers.ToList();
        this.parts = parts.ToList();
        Date = date;
        ID = IDGen(ascBooking, date);
    }

    public void addPart(Part part) { parts.Add(part);}
    
    public string removePart(Part pt)
    {
        return !parts.Remove(parts.Find(part => part.ID == pt.ID)) ? $"Part with ID {pt.ID} not found in repair list." : $"Part with ID {pt.ID} removed from repair list.";
    }
    
    public decimal updateLab(decimal newLab)
    {
        LabCost = newLab;
        return LabCost;
    }

    public override string IDGen(Booking booking, DateTime date)
    {
        StringBuilder id = new();
        id.AppendFormat($"R-{booking.ID}-{date}");
        return id.ToString();    
    }

    public override string ToString()
    {
        return $"[Repair ID: [{ID}] | Booking: {ascBooking.ID} | Date: [{Date:dd/MM/yyyy hh:mm} | Description: {Description} | Total Cost: {TotalCost:C} - ( Of which:  Parts Cost: {PartsCost:C}, Labour: {LabCost:C} ) | Repairers involved: {repairers.Count} | Parts count: {parts.Count}]";
    }
}