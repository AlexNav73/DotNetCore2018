using DotNetCore2018.Core;

namespace DotNetCore2018.Data.Entities
{
    public sealed class Supplier : IHasId<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}