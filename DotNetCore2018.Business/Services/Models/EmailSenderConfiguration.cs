namespace DotNetCore2018.Business.Services.Models
{
    public class EmailSenderConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSSL { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}