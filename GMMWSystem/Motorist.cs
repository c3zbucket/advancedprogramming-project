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
    public string ID { get; set; }
    public string Name { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }
    
    public Motorist(string id, string name, string email, string? phoneNo)
    {
        ID = id;
        Name = name;
        Email = email;
        PhoneNo = phoneNo;
    }
    
}