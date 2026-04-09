namespace GMMWSystem;

public class Student : IStaff
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }

    public Student(string id, string name, string phoneNo, string email)
    {
        ID = id;
        Name = name;
        PhoneNo = phoneNo;
        Email = email;
    }
}