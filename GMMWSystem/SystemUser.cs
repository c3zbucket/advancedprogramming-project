using System.Security.AccessControl;

namespace GMMWSystem;

public class SystemUser 
{
    private Staff member;

    private string password;

    public Staff Member { get => member; set => member = value; }

    public string Password { get => password; set => password = value; }

    public Role role { get; set; }
    
}