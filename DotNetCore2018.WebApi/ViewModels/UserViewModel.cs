using System;

namespace DotNetCore2018.WebApi.ViewModels
{
    public class UserViewModel
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string Roles { get; set; }
    }
}
