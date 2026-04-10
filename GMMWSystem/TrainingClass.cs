using System.Text;

namespace GMMWSystem;

public class TrainingClass
{
    public string ID { get; set; }

    public Student student { get; set; }
    public string Name { get; set; }
    public ClassType ClassType { get; set; }
    public string Description { get; set; }
    public List<Motorist> attendees { get; set; }
    public DateTime Date { get; set; }
    
}