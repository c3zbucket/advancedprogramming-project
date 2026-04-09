namespace GMMWSystem;

public class Lecturer : IStaff
{
    public string ID { get; set; }
    public string Name { get; set; }
    public string PhoneNo { get; set; }
    public string Email { get; set; }

    public Lecturer(string id, string name, string phoneNo, string email)
    {
        ID = id;
        Name = name;
        PhoneNo = phoneNo;
        Email = email;
    }
}
