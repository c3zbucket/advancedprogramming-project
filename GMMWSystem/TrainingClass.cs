using System.Text;

namespace GMMWSystem;

public class TrainingClass : Record<ClassType,DateTime>
{
    public override string ID { get; }

    public override string IDGen(ClassType type, DateTime date)
    {
        StringBuilder id = new();
        id.AppendFormat($"TC-{type}-{date:yyyyMMddHHmm}");
        return id.ToString();            
    }

    public Student student { get; private set; }
    public string Name { get; set; }
    public ClassType ClassType { get; set; }
    public string Description { get; set; }
    public List<Motorist> attendees { get; private set; }
    public DateTime Date { get; set; }

    public TrainingClass(Student student, string name, ClassType classType, DateTime date, IEnumerable<Motorist> motorists, string? description)
    {
        this.student = student;
        this.Name = name;
        this.ClassType = classType;
        this.attendees = motorists.ToList();
        this.Date = date;
        this.Description = description ?? "No description"; //If no description is provided, set default description
        ID = IDGen(classType, date);
    }
    
    public string register(Motorist motorist)
    {
        try
        {
            attendees.Add(motorist);
        }
        catch (Exception e)
        {
            Console.WriteLine("could not register");
        }

        return "Motorist registered to class";
    }

    public string remove(Motorist motorist)
    {
        try
        {
            attendees.Remove(motorist);
        }
        catch (Exception e)
        {
            return "could not remove";
        }
        return "Motorist removed";
    }
    
    public string reschedule(DateTime newtime)
    {
        try
        {
            Date = newtime;
        }
        catch (Exception e)
        {
            Console.WriteLine("could not change time");
        }

        return "Time changed";
    }
    
    public int Count() => attendees.Count;

    public override string ToString() =>
        $"Class ID: [{ID}] | Name: {Name} | Type: {ClassType} | Date: {Date:dd/MM/yyyy HH:mm} | Taught by: {student.Name} | Attendees: {attendees.Count} | Description: {Description}";
}