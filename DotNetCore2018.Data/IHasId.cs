namespace DotNetCore2018.Data
{
    public interface IHasId<T>
    {
        T Id { get; set; }
    }
}