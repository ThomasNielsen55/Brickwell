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

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void RemoveOrder(Order delete)
        {
            _context.Orders.Remove(delete);
            _context.SaveChanges();
        }
        public void UpdateOrder(Order update)
        {
            _context.Orders.Update(update);
            _context.SaveChanges();
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void RemoveProduct(Product delete)
        {
            _context.Products.Remove(delete);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product update)
        {
            _context.Products.Update(update);
            _context.SaveChanges();
        }
        public void AddUser(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        public void RemoveUser(Customer delete)
        {
            _context.Customers.Remove(delete);
            _context.SaveChanges();
        }
        public void UpdateUser(Customer update)
        {
            _context.Customers.Update(update);
            _context.SaveChanges();
        }
    }
}