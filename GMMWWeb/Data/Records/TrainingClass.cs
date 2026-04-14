using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
namespace GMMWWeb.Data.Records;
using Enums;

public class TrainingClass
{
    [Required]
    public string ID { get; set; }
    [Required]
    public string StudentID { get; set; }
    [DisplayName("Taught by:")]
    public Student student { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    [DisplayName("Class Type")]
    public ClassType ClassType { get; set; }
    public string Description { get; set; }
    [DisplayName("Attendees")]
    public List<Motorist> attendees { get; set; }
    [Required]
    public DateTime Date { get; set; }
    
}