namespace Brickwell.Data.ViewModels
{
    public class ProductViewdude
    {
        public Product selectedProduct {  get; set; }
        public IQueryable<Product> Products { get; set; }

    }
}
