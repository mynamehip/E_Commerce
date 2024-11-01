using EC.DataAccess.Repository.IRepository;
using EC.Models.Models;
using EC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace E_Commerce.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, IEmailSender emailSender)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claimId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);;
            if (claimId == null)
            {
                HttpContext.Session.SetInt32(SD.SessionCart, 0);
            }
            else
            {
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == claimId.Value).Count());
            }
            IEnumerable<Category> categories = _unitOfWork.CategoryRepository.GetAll();
            ViewData["Categories"] = categories;
            return View();
        }

        public async Task<IActionResult> SendTestEmail()
        {
            try
            {
                await _emailSender.SendEmailAsync("hiphkiiio@gmail.com", "Test Subject", "Test Message");
                ViewData["Message"] = "Email đã được gửi thành công!";
            }
            catch (Exception ex)
            {
                ViewData["Message"] = $"Gửi email thất bại: {ex.Message}";
            }
            return RedirectToAction(nameof(Index));
        }


        public IActionResult GetProducts(int? id, int? pageNumber, string? name)
        {
            int pageSize = 8;
            IEnumerable<Product> products;

            if (id != null)
            {
                products = _unitOfWork.ProductRepository
                            .GetAll(includeProperties: "Category,ProductImages")
                            .Where(u => u.CategoryId == id);
            }
            else
            {
                products = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category,ProductImages");
            }

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Title.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            var paginatedList = Pagination<Product>.Create(products.AsQueryable(), pageNumber ?? 1, pageSize);
            return PartialView("_ProductListPartial", paginatedList);
        }


        public IActionResult Detail(int productId)
        {
            ShoppingCart cart = new ShoppingCart()
            {
                Product = _unitOfWork.ProductRepository.Get(u => u.Id == productId, includeProperties: "Category,ProductImages"),
                Count = 1,
                ProductId = productId
            };
            return View(cart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Detail(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCartRepository.Get(u => u.ApplicationUserId == userId &&
            u.ProductId == shoppingCart.ProductId);

            if (cartFromDb != null)
            {
                //shopping cart exists
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCartRepository.Update(cartFromDb);
                _unitOfWork.Save();
            }
            else
            {
                //add cart record
                _unitOfWork.ShoppingCartRepository.Add(shoppingCart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.ShoppingCartRepository.GetAll(u => u.ApplicationUserId == userId).Count());
            }
            TempData["success"] = "Cart updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
