using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Brickwell.Data;
using Microsoft.AspNetCore.Mvc;
using Brickwell.Data.ViewModels;

namespace Brickwell.Components
{
	public class ColorViewComponent : ViewComponent
	{
		private IBrickRepository _brickRepo;
		public ColorViewComponent(IBrickRepository temp)
		{
			_brickRepo = temp;
		}
		public IViewComponentResult Invoke()
		{
            ViewBag.SelectedPrimaryColor = RouteData?.Values["PrimaryColor"];
            ViewBag.SelectedCategory = RouteData?.Values["Category"];

            var viewModel = new FilterViewModel
            {
                Categories = _brickRepo.Products
         .Select(x => x.Category ?? "")
         .Where(x => !string.IsNullOrWhiteSpace(x))
         .Distinct()
         .OrderBy(x => x),

                PrimaryColors = _brickRepo.Products
         .Select(x => x.PrimaryColor ?? "")
         .Where(x => !string.IsNullOrWhiteSpace(x))
         .Distinct()
         .OrderBy(x => x)
            };


            //foreach (var m in viewModel.Categories)
            //{
            //    m = m.Split('-').FirstOrDefault();
            //}
            var modifiedCategories = viewModel.Categories.Select(m => m.Split(" - ").FirstOrDefault()).Distinct().ToList();

            viewModel.Categories = modifiedCategories;


            return View(viewModel);
        }




	}
}
