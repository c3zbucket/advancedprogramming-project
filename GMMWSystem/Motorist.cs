namespace GMMWSystem;

public class Motorist : IPerson
{
    private string id;
    public string ID
    {
        get => id;
        set => id = value;
    }

    public string name;
    public string Name
    {
        get => name;
        set => name = value;
    }

    private string phoneNo;
    public string PhoneNo 
    {
        get => phoneNo;
        set => phoneNo = value;
    }

    private string email;
    public string Email {
        get => email;
        set => email = value;
    }
    
}