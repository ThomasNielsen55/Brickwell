namespace Brickwell.Data
{
    public interface IBrickRepository
    {
        public IQueryable<Customer> Customers { get; }
        public IQueryable<LineItem> LineItems { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<Product> Products { get; }
    }
}
