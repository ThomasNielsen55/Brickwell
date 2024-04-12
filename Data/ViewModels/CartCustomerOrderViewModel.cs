using Brickwell.Pages;

namespace Brickwell.Data.ViewModels
{
	public class CartCustomerOrderViewModel
	{
		public IQueryable<Order>? Orders { get; set; }
		public IQueryable<Customer>? Customers { get; set; }
		public IQueryable<Product>? Products { get; set; }
		public Cart? Cart { get; set; }
	}
}
