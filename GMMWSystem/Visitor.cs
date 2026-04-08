namespace GMMWSystem;

public class Visitor : IStaff
{
    private string id;
    String ID { get; }

    private string name;
    String Name { get; }

    private string phoneNo;
    String PhoneNo { get; set; }

    private string email;
    String Email { get; set; }   
}