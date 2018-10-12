namespace DotNetCore2018.Core
{
    public interface IHasId<T>
    {
        T Id { get; set; }
    }
}