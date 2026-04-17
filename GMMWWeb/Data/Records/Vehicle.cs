using System.ComponentModel.DataAnnotations;
using System.Text;
using GMMWWeb.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace GMMWWeb.Data.Records;
using Enums;

public class Vehicle
{
    [Display(Name = "Registration Plate")]
    [RegularExpression(@"^[A-Z]{2}\d{2}\s[A-Z]{3}$", ErrorMessage = "Registration plate must be in the format 'AB12-CDE'")]
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
    
    [YearHelper(1990)]
    public int Year { get; set; }
}