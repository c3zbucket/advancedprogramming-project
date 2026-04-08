using System.Runtime.InteropServices.JavaScript;

namespace GMMWSystem;

public class TrainingClass
{
    private string id;
    public string ID
    {
        get => id;
        set => id = value;
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
    
    private string desc;
    public string Description
    {
        get => desc;
        set => desc = value;
    }

    public List<Motorist> attendees;

    public DateTime date;

    public DateTime Date
    {
        get => date;
        set => date = value;
    }

    public TrainingClass(string id, Student student, string name, DateTime date, IEnumerable<Motorist> motorists)
    {
        
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
            Console.WriteLine("could not remove");
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

}