namespace GMMWSystem;

/**
 * New Generic 'Person' interface that encompasses all types of people at the workshop
 */
public interface IPerson
{
    public string ID
    {
        get;
        set;
    }
    
    public string Name
    {
        get;
        set;
    }

    public string PhoneNo
    {
        get;
        set;
    }

    public string Email
    {
        get;
        set;
    }
    
}