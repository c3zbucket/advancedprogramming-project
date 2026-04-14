using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace GMMWWeb.Data.Records;
using Enums;

public class Vehicle
{
    [Required]
    [Display(Name = "Registration Plate")]
    public string ID { get; set; }
    [Required]
    public string ownerID { get; set; }
    [Display(Name = "Vehicle Owner")]
    public Motorist owner { get; set; }
    [Required]
    public string Make { get; set; }
    public string Model { get; set; }
    [Required]
    public Transmission transmission { get; set; } 
    [Required]
    public Engine engine { get; set; }
    [Required]
    public string Year { get; set; }
}