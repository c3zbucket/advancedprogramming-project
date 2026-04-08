using System.Data.Common;
using System.Runtime.InteropServices.JavaScript;

namespace GMMWSystem;

public class Repair
{
    private Booking ascBooking;
    
    private string description;

    public List<Student> repairers;

    private decimal partsCost;

    public List<Part> parts;
    
    public DateTime date;
    
    protected decimal labCost;
    protected Decimal LabCost { get => labCost; set => labCost = value; }
    
    private decimal totalCost;
    private decimal TotalCost { get => totalCost;}
    
    public Repair(Booking ascBooking, string description, decimal partsCost, decimal labCost, IEnumerable<Student>repairers, IEnumerable<Part>parts)
    {
        this.ascBooking = ascBooking;
        this.description = description;
        this.partsCost = partsCost;
        this.labCost = labCost;
        this.repairers = repairers.ToList();
        this.parts = parts.ToList();
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
        return newLab;
    }
    
    public decimal updateTotal(decimal newTotal)
    {
        return newTotal;
    }

    public override String ToString()
    {
        return "Repair";
    }
}