using System.Text;

namespace GMMWSystem;

public static class RecordServices
{
    public static string IDGen(ClassType type, DateTime date)
    {
        StringBuilder id = new();
        id.AppendFormat($"TC-{type}-{date:yyyyMMddHHmm}");
        return id.ToString();            
    }
    
    
    /*
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
    
    public string LinkedRepairs()
    {
        if (Repairs.Count == 0) return "No repairs linked to this booking";
        StringBuilder repairList = new();
        Repairs.ForEach(repair => repairList.AppendLine(repair.ToString()));
        return repairList.ToString();
    }

    
    public string removePart(Part pt)
       {
           return !parts.Remove(parts.Find(part => part.ID == pt.ID)) ? $"Part with ID {pt.ID} not found in repair list." : $"Part with ID {pt.ID} removed from repair list.";
       }
    
    public int Count() => attendees.Count;

    public override string ToString() =>
        $"Class ID: [{ID}] | Name: {Name} | Type: {ClassType} | Date: {Date:dd/MM/yyyy HH:mm} | Taught by: {student.Name} | Attendees: {attendees.Count} | Description: {Description}";
    */
}