using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Brickwell.Data;
using Brickwell.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Brickwell.Pages
{
    public class CartModel : PageModel
    {
        private IBrickRepository _brickRepository;
        public CartModel(IBrickRepository temp) 
        {
            _brickRepository = temp;
        }
        public Cart? Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";
        public void OnGet(string returnUrl, bool delete)
        {
            if (delete)
            {
				Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

                Cart.Clear();
				HttpContext.Session.SetJson("cart", Cart);

			}
			else
            {
                ReturnUrl = returnUrl ?? null;
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
            }
        }
        public IActionResult OnPost(int productId, string returnUrl, bool delete)
        {
            if (delete)
            {
				Product prod = _brickRepository.Products
				.FirstOrDefault(x => x.ProductId == productId);

				if (prod != null)
				{
					Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
					Cart.RemoveLine(prod);
					HttpContext.Session.SetJson("cart", Cart);
					return RedirectToPage(new { returnUrl = ReturnUrl });
				}
				return RedirectToPage(new { returnUrl = ReturnUrl });
			}
            else
            {
                Product prod = _brickRepository.Products
                    .FirstOrDefault(x => x.ProductId == productId);

                if (prod != null)
                {
                    Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                    Cart.AddItem(prod, 1);
                    HttpContext.Session.SetJson("cart", Cart);
                }

                return RedirectToPage(new { returnUrl = returnUrl });
            }
        }
  //      public IActionResult OnDelete(int productId)
  //      {
  //          Product prod = _brickRepository.Products
  //              .FirstOrDefault(x => x.ProductId == productId);

  //          if (prod != null) 
  //          {
  //              Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
  //              Cart.RemoveLine(prod);
  //              HttpContext.Session.SetJson("cart", Cart);
  //              return RedirectToPage(new { returnUrl = ReturnUrl });
  //          }
		//	return RedirectToPage(new { returnUrl = ReturnUrl });
		//}
    }
}
