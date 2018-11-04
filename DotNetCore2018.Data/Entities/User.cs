namespace DotNetCore2018.Data.Entities
{
    public class User
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string HashedPassword { get; set; }
    }
}