using Brickwell.Pages;

namespace Brickwell.Data.ViewModels
{
	public class CartCustomerOrderViewModel
	{
		public Order Order { get; set; }
		public Customer Customer { get; set; }
		public Product Product { get; set; }
		public Cart? Cart { get; set; }
	}
}
