using System.Security.AccessControl;

namespace GMMWSystem;

public class SystemUser 
{
    private IStaff member;

    private string password;

    public IStaff Member { get => member; set => member = value; }

    public string Password { get => password; set => password = value; }

    public Role role { get => role; set => role = value; }
    
}