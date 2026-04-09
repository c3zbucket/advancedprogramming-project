using System.Text;

namespace GMMWSystem;

public class Part : Record<String, PartType>
{
    public override string ID { get; }

    public string Make { get; private set; }
    public string Description { get; private set; }
    public PartType Type { get; private set; }
    public decimal Cost { get; set; }

    public Part(string make, PartType type, decimal cost, string? desc)
    {
        this.Make = make;
        this.Type = type;
        this.Cost = cost;
        this.Description = desc ?? string.Empty;
        ID = IDGen(make, type);
    }

    public override string IDGen(String make, PartType partType)
    {
        StringBuilder id = new();
        id.AppendFormat($"P-{make}-{partType.GetTypeCode()}");
        return id.ToString();        }

    public override string ToString() =>
        $"Part[{ID}] | Make: {Make} | Type={Type} | Cost: {Cost:C} | Desc: {Description}";
}