using EC.DataAccess.Repository.IRepository;
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

        public IViewComponentResult Index()
        {

            return View();
        }
    }
}
