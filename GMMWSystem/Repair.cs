using System.Text;

namespace GMMWSystem;

public class Repair : Record<Booking,DateTime>
{
    public override string ID { get; }

    private Booking ascBooking;
    
    private string description;

    public List<Student> repairers;

    private decimal partsCost;

    public List<Part?> parts;
    
    public DateTime Date;
    
    protected decimal labCost;
    protected Decimal LabCost { get => labCost; set => labCost = value; }
    private decimal TotalCost { get => partsCost + labCost;}
    
    public Repair(Booking ascBooking, string description, decimal partsCost, decimal labCost, IEnumerable<Student>repairers, IEnumerable<Part>parts, DateTime date)
    {
        ID = IDGen(ascBooking,date);
        this.ascBooking = ascBooking;
        this.description = description;
        this.partsCost = partsCost;
        this.labCost = labCost;
        this.repairers = repairers.ToList();
        this.parts = parts.ToList();
        Date = date;
    }

    public void addPart(Part part) { parts.Add(part);}
    
    public string removePart(Part pt)
    {
        return !parts.Remove(parts.Find(part => part.ID == pt.ID)) ? $"Part with ID {pt.ID} not found in repair list." : $"Part with ID {pt.ID} removed from repair list.";
    }
    
    public decimal updateLab(decimal newLab)
    {
        labCost = newLab;
        return labCost;
    }

    public override string IDGen(Booking booking, DateTime date)
    {
        StringBuilder id = new();
        id.AppendFormat($"R-{booking.ID}-{date}");
        return id.ToString();    
    }

    public override string ToString()
    {
        return $"[Repair ID: [{ID}] | Booking: {ascBooking.ID} | Date: [{Date:dd/MM/yyyy hh:mm} | Description: {description} | Total Cost: {TotalCost:C} - ( Of which:  Parts Cost: {partsCost:C}, Labour: {labCost:C} ) | Repairers involved: {repairers.Count} | Parts count: {parts.Count}]";
    }
}