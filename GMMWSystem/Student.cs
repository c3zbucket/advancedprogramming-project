namespace GMMWSystem;

public class Student : IStaff
{
    private String _id;
    public String ID
    {
        get => _id;
        set => _id = value;
    }
    private String _name;

    public String Name
    {
        get => _name;
        set => _name = value;
    }

    private String _phoneNo;
    public String PhoneNo 
    {
        get => _phoneNo;
        set => _phoneNo = value;
    }

    private String _email;
    public String Email {
        get => _email;
        set => _email = value;
    }

    public Student(string  id, string name, string phoneNo, string email)
    {
        
    }
}