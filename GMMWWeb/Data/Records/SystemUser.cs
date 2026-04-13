using System.Security.AccessControl;
namespace GMMWWeb.Data.Records;
using Enums;

public class SystemUser 
{
    public string ID { get; set;}
    
    public Staff Member { get; set; }

    public string Password { get; set; }

    public Role Role { get; set; }
    
}