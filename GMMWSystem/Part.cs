namespace GMMWSystem;

public class Part
{
    private string id;
    private string Id
    {
        get => id;
    }

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

    public DateTime used;
    public DateTime Used
    {
        get => used;
        protected set => used = value;
    }
}