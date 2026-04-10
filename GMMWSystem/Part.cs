using System.Text;

namespace GMMWSystem;

public class Part
{
    public string ID { get; set; }

    public string Make { get; set; }
    public string Description { get; set; }
    public PartType Type { get; set; }
    public decimal Cost { get; set; }

    // Parameterless constructor for EF Core
    public Part() {}

    public Part(string make, PartType type, decimal cost, string? desc)
    {
        this.Make = make;
        this.Type = type;
        this.Cost = cost;
        this.Description = desc ?? string.Empty;
        ID = IDGen(make, type);
    }

    public string IDGen(String make, PartType partType)
    {
        StringBuilder id = new();
        id.AppendFormat($"P-{make}-{partType.GetTypeCode()}");
        return id.ToString();        }

    public override string ToString() =>
        $"Part[{ID}] | Make: {Make} | Type={Type} | Cost: {Cost:C} | Desc: {Description}";
}