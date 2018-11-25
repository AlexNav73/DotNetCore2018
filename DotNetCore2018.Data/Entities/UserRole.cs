using Microsoft.AspNetCore.Identity;
using System;

namespace DotNetCore2018.Data.Entities
{
    public class UserRole : IdentityRole<Guid>
    {
        public UserRole() : base()
        {
        }

        public UserRole(string roleName) : base(roleName)
        {
        }
    }
}
