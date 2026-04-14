using System.ComponentModel.DataAnnotations;

namespace GMMWWeb.Data.Records;

/**
 * Motorist class which implements the more general IPerson interface
 * 
 */
public class Motorist : IPerson
{
    // ID of the motorist
    [Required]
    public string ID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string PhoneNo { get; set; }
    public string Email { get; set; }
}