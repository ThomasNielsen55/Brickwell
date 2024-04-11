namespace Brickwell.Data
{
    public interface IBrickRepository
    {
        public IQueryable<Customer> Customers { get; }
        public IQueryable<LineItem> LineItems { get; }
        public IQueryable<Order> Orders { get; }
        public IQueryable<Product> Products { get; }

        public void AddOrder(Order order);
        public void RemoveOrder(Order delete);
        public void UpdateOrder(Order update);
        public void AddProduct(Product product);
        public void RemoveProduct(Product delete);
        public void UpdateProduct(Product update);
        public void AddUser(Customer customer);
        public void RemoveUser(Customer delete);
        public void UpdateUser(Customer update);
    }
}
