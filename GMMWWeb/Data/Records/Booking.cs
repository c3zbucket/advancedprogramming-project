using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GMMWWeb.Data.Records;

public class Booking
{
    [Required]
    public string ID { get; set; }
    [Required]
    public string bookedPlate { get; set; }
    public Vehicle bookedVehicle { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public DateTime Date { get; set; }
    public TimeSpan? TimeTaken { get; set; }
    public List<Repair>? Repairs { get; set; } = new();
}