namespace DotNetCore2018.WebApi.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CategoryViewModel Category { get; set; }
        public SupplierViewModel Supplier { get; set; }
    }
}