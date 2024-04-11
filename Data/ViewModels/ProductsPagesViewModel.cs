namespace Brickwell.Data.ViewModels
{
    public class ProductsPagesViewModel 
    {
        public IQueryable<Product> Products { get; set; }
        public string? CurrentColor { get; set; }
        public string? CurrentCategory { get; set; }
        public PaginationInfo PaginationInfo { get; set; } = new PaginationInfo();
    }
}
