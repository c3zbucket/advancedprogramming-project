namespace GMMWSystem;

public class Student : IStaff
{
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

    public Student(string id, string name, string phoneNo, string email)
    {
        this.id = id;
        this.name = name;
        this.phoneNo = phoneNo;
        this.email = email;
    }
}