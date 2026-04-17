using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json.Serialization;

namespace GMMWWeb.Data.Records;

/**
 * Motorist class which implements the more general IPerson interface
 * 
 */
public class Motorist : IPerson
{
    // ID of the motorist
    public string ID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
// Phone number must be from UK, so 01/2 for landline or 07 for mobile phone
    [RegularExpression(@"^0[127]\d{9}$", ErrorMessage = "Phone number be valid UK phone number")]
    public string PhoneNo { get; set; }
    [RegularExpression(@"^[^@]+@[^@]+\.(com|co\.uk)$", ErrorMessage = "Email must be a valid email address")]
    public string Email { get; set; }
}