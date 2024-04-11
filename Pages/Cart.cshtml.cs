using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Brickwell.Data;
using Brickwell.Infrastructure;

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
        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? null;
            Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }
        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product prod = _brickRepository.Products
                .FirstOrDefault(x => x.ProductId == productId);

            if (prod != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(prod, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
                        
            return RedirectToPage (new {returnUrl = returnUrl});
        }
    }
}
