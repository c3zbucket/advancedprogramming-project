using System.Security.AccessControl;

namespace GMMWSystem;

public class SystemUser 
{
    private IStaff member;

    private string password;

    public IStaff Member
    {
        get => member;
        set => member = value;
    }

    public string Password
    {
        get => password;
        set => password = value;
    }

    public Role role
    {
        get => role;
        set => role = value;
    }

    public SystemUser(IStaff member, Role role, String password)
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
                if (newpass != confirm)
                {
                    Console.WriteLine("Passwords are incorrect, try again");
                    password = newpass;
                    Console.WriteLine("Password changed");
                    return;
                }
                password = newpass;
                Console.WriteLine("Password changed");
                break;
        }
    }
}