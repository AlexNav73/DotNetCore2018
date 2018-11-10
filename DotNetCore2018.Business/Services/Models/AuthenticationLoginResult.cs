using System.Collections.Generic;

namespace DotNetCore2018.Business.Services.Models
{
    public sealed class AuthenticationLoginResult
    {
        public bool Success { get; set; }
        public IList<AuthenticationLoginError> Errors { get; set; }
    }

    public sealed class AuthenticationLoginError
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}