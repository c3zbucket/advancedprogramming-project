namespace GMMWSystem;

public class Repair
{
    private Booking ascBooking;

    private string description;

    public List<Student> repairers;

    private decimal partsCost;

    public List<Part> parts;
    
    //public Decimal PartsCost { return parts }
    
    private decimal labCost;
    public Decimal LabCost { get => labCost; set => labCost = value; }
    private decimal totalCost;
    
    public decimal TotalCost { get => totalCost;}
    
    public Repair(Booking booking, String description)
    {
        ascBooking = booking;
    }

    public string addPart(string id)
    {
        return "Part added";
    }
    
    public string removePart(string id)
    {
        return "Part added";
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