using EC.DataAccess.Repository.IRepository;
using EC.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ViewComponents
{
    public class HomeCarouselViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeCarouselViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Product> highLightList = _unitOfWork.ProductRepository.GetAll(u => u.Highlight == true, includeProperties: "Category,ProductImages").ToList();
            return View(highLightList);
        }
    }
}
