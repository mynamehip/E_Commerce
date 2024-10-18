using EC.DataAccess.Data;
using EC.DataAccess.Repository.IRepository;
using EC.Models.Models;
using EC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_User_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> categories = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(categories);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == "test addmodelerror")
            {
                ModelState.AddModelError("Name", "Test Success");
                return View();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Update(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? obj = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
            if (obj == null) return NotFound();
            return View(obj);
        }
        [HttpPost]
        public IActionResult Update(Category obj)
        {
            if (obj.Name == "test addmodelerror")
            {
                ModelState.AddModelError("Name", "Test Success");
                return View();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CategoryRepository.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        public IActionResult Remove(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? obj = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
            if (obj == null) return NotFound();
            return View(obj);
        }
        [HttpPost, ActionName("Remove")]
        public IActionResult RemoveAction(int? id)
        {
            Category? obj = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CategoryRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category removed successfully";
            return RedirectToAction("Index", "Category");
        }
    }
}
