using System.Text;

namespace GMMWSystem;

public class Repair
{
    public string ID { get; set; }
    public Booking ascBooking { get; set; }
    public string Description { get; set; }
    public List<Student> repairers { get; set; }
    public decimal PartsCost { get; set; }
    public List<Part> parts { get; set; }
    public DateTime Date { get; set; }
    public decimal LabCost { get; set; }
    public decimal TotalCost => PartsCost + LabCost;
}