namespace Brickwell.Data
{
    public class EFBrickRepository : IBrickRepository
    {
        private BrickDbContext _context;
        public EFBrickRepository(BrickDbContext temp)
        {
            _context = temp;
        }
        public IQueryable<Customer> Customers => _context.Customers;
        public IQueryable<LineItem> LineItems => _context.LineItems;
        public IQueryable<Order> Orders => _context.Orders;
        public IQueryable<Product> Products => _context.Products;
    }
}