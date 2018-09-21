namespace DotNetCore2018.Data.Entities
{
    public sealed class Category : IHasId<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}