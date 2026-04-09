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

    private Student student;

    private string name;
    public string Name
    {
        get => name;
        set => name = value;
    }

    public ClassType classType;

    public ClassType ClassType
    {
        get => classType;
        set => classType = value;
    }
    
    private string description;
    public string Description
    {
        get => description;
        set => description = value;
    }

    public List<Motorist> attendees;

    public DateTime date;

    public DateTime Date
    {
        get => date;
        set => date = value;
    }

    public TrainingClass(Student student, string name, ClassType classType, DateTime date, IEnumerable<Motorist> motorists, string? description)
    {
        ID = IDGen(classType, date);
        this.student = student;
        this.name = name;
        this.classType = classType;
        attendees = motorists.ToList();
        this.date = date;
        this.description = description ?? "No description"; //If no description is provided, set default description
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
            date = newtime;
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