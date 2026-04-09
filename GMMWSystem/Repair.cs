using System.Data.Common;
using System.Runtime.InteropServices.JavaScript;
using System.Text;

namespace GMMWSystem;

public class Repair : Record<Booking,DateTime>
{
    public override string ID { get; }

    private Booking ascBooking;
    
    private string description;

    public List<Student> repairers;

    private decimal partsCost;

    public List<Part> parts;
    
    public DateTime Date;
    
    protected decimal labCost;
    protected Decimal LabCost { get => labCost; set => labCost = value; }
    
    private decimal totalCost;
    private decimal TotalCost { get => totalCost;}
    
    public Repair(Booking ascBooking, string description, decimal partsCost, decimal labCost, IEnumerable<Student>repairers, IEnumerable<Part>parts, DateTime date)
    {
        ID = IDGen(ascBooking,date);
        this.ascBooking = ascBooking;
        this.description = description;
        this.partsCost = partsCost;
        this.labCost = labCost;
        this.repairers = repairers.ToList();
        this.parts = parts.ToList();
        this.Date = date;
        this.totalCost = partsCost + labCost;
    }

    public bool addPart(string id)
    {
        return true;
    }
    
    public bool removePart(string id)
    {
        return false;
    }
    
    public decimal updateLab(decimal newLab)
    {
        labCost = newLab;
        totalCost = partsCost + labCost;
        return labCost;
    }
    
    public decimal updateTotal(decimal newTotal)
    {
        totalCost = newTotal;
        return totalCost;
    }

    public override string IDGen(Booking booking, DateTime date)
    {
        StringBuilder id = new();
        id.AppendFormat($"R-{booking.ID}-{date}");
        return id.ToString();    
    }

    public override string ToString()
    {
        return $"Repair ID: [{ID}] | Booking: {ascBooking.ID} | Date: [{Date:dd/MM/yyyy} Desc={description} Total Cost: {totalCost:C} - ( Of which:  Parts Cost: {partsCost:C}, Labour: {labCost:C} ) | Repairers involved: {repairers.Count} Parts count: {parts.Count}";
    }
}