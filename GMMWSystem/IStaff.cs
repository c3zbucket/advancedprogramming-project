namespace GMMWSystem;

public interface IStaff : IPerson
{
    public String ID
    {
        get => id;
        set => id = value;
    }
    public String Name
    {
        get => name;
        set => name = value;
    }
    public String PhoneNo 
    {
        get => phoneNo;
        set => phoneNo = value;
    }

    public String Email {
        get => email;
        set => email = value;
    }
}