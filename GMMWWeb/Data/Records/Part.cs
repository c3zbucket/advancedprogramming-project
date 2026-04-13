using System.Text;

namespace GMMWWeb.Data.Records;
using Enums;

public class Part
{
    public string ID { get; set; }
    public string Make { get; set; }
    public string Description { get; set; }
    public PartType Type { get; set; }
    public decimal Cost { get; set; }
}