using System.Security.AccessControl;

namespace GMMWSystem;

public class SystemUser : IStaff
{
    private IStaff member;

    private String password;
    
    public IStaff Member
    {
        get => member;
        set => member = value;
    }
    
    public String Password
    {
        get => password;
        set => password = value;
    }

    public Role role
    {
        get => role;
        set => role = value;
    }
    
    public SystemUser(IStaff member, String password)
    {
        this.member = member;
        this.password = password;
    }

    public void updateRecord(int arg)
    {
        switch (arg)
        {
            case 1:
                Console.WriteLine("Enter new password: ");
                string newpass = Console.ReadLine();
                Console.WriteLine("Confirm new password: ");
                string confirm = Console.ReadLine();
                Console.WriteLine(newpass = confirm : "nah" ? "no");
                Console.WriteLine("Passsord changed");
            case 2:
                
                
            case 3:
                
                
            case 4:
                
                
            case 4:
        }
    }
}