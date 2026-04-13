namespace GMMWWeb.Data.Records;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

public abstract class Staff : IPerson
{
    public string ID { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [DisplayName("Phone Number")]
    public string PhoneNo { get; set; }
    [Required]
    [DisplayName("Email Address")]
    public string Email { get; set; }
}