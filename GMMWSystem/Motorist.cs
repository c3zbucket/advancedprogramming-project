namespace GMMWSystem;

/**
 * Motorist class which implements the more general IPerson interface
 * 
 */
public class Motorist : IPerson
{
    /*
     * ID of the motorist
     */
    private string id;
    public string ID
    {
        get => id;
        set => id = value;
    }

    private string name;
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
    
    public Motorist(string id, string name, string email, string? phoneNo)
    {
        this.id = id;
        this.name = name;
        this.email = email;
        this.phoneNo = phoneNo;
    }
    
}