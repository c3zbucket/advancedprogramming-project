namespace GMMWSystem;

/**
 * New Generic 'Person' interface that encompasses all types of people at the workshop
 */
public interface IPerson
{
    private String id;
    public String ID
    {
        get => id;
        set => id = value;
    }
    private String name;

    public String Name
    {
        get => name;
        set => name = value;
    }

    private String phoneNo;
    public String PhoneNo 
    {
        get => phoneNo;
        set => phoneNo = value;
    }

    private String email;
    public String Email {
        get => email;
        set => email = value;
    }
    
}