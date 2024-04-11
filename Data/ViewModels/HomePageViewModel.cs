namespace Brickwell.Data.ViewModels
{
    public class HomePageViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public IQueryable<Customer> Customers { get; set; }
        public Customer CurrentCustomer { get; set; }
    }
}
