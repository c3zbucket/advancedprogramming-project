namespace GMMWSystem;

public abstract class Staff : IPerson
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
}