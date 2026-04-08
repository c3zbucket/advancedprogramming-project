namespace GMMWSystem;

public class Part
{
    private string id;
    public string Id
    {
        get => id;
    }

    private string make;
    public string Make
    {
        get => make;
        set => make = value;
    }

    private string desc;
    public string Description
    {
        get => desc;
        set => desc = value;
    }

    private PartType type;
    public PartType Type
    {
        get => type;
    }
    
    private decimal cost;
    public decimal Cost
    {
        get => cost;
        set => cost = value;
    }
    
}