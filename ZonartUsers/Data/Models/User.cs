using Microsoft.AspNetCore.Identity;

namespace ZonartUsers.Data.Models
{
    public class User : IdentityUser
    {   
        public string FullName { get; set; }   
        
    }
}
