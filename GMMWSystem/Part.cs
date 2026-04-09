using System.Reflection;
using System.Text;

namespace GMMWSystem;

public class Part : Record<String, PartType>
{
    public override string ID { get; }

    private string make;
    private string Make
    {
        get => make;
        set => make = value;
    }

    private string desc;
    private string Description
    {
        get => desc;
        set => desc = value;
    }

    private PartType type;
    private PartType Type
    {
        get => type;
    }
    
    private decimal cost;
    public decimal Cost
    {
        get => cost;
        set => cost = value;
    }

    public Part(string make, PartType type, decimal cost, string? desc)
    {
        ID = IDGen(make,type);
        this.make = make;
        this.type = type;
        this.cost = cost;
        this.desc = desc ?? string.Empty;
    }

    public override string IDGen(String make, PartType partType)
    {
        StringBuilder id = new();
        id.AppendFormat($"P-{make}-{partType.GetTypeCode()}");
        return id.ToString();        }

    public override string ToString() =>
        $"Part[{ID}] | Make: {make} | Type={type} | Cost: {cost:C} | Desc: {desc}";
}