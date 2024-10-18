using EC.DataAccess.Data;
using EC.DataAccess.Repository.IRepository;
using EC.Models.Models;
using EC.Models.ViewModels;
using EC.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace E_Commerce.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_User_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> Companys = _unitOfWork.CompanyRepository.GetAll().ToList();
            return View(Companys);
        }

        public IActionResult Upsert(int? id)
        {
            if(id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company obj = _unitOfWork.CompanyRepository.Get(u => u.Id == id);
                return View(obj);
            }
        }
        [HttpPost]
        public IActionResult Upsert(Company obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Id != 0)
                {
                    _unitOfWork.CompanyRepository.Update(obj);
                    TempData["success"] = "Company updated successfully";
                }
                else
                {
                    _unitOfWork.CompanyRepository.Add(obj);
                    TempData["success"] = "Company created successfully";
                }
                _unitOfWork.Save();
                return RedirectToAction("Index", "Company");
            }
            else
            {
                return View(obj);
            }
        }

        public IActionResult Remove(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Company? obj = _unitOfWork.CompanyRepository.Get(u => u.Id == id);
            if (obj == null) return NotFound();
            return View(obj);
        }
        [HttpPost, ActionName("Remove")]
        public IActionResult RemoveAction(int? id)
        {
            Company? obj = _unitOfWork.CompanyRepository.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.CompanyRepository.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Company removed successfully";
            return RedirectToAction("Index", "Company");
        }

        #region CALL API
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> Companys = _unitOfWork.CompanyRepository.GetAll().ToList();
            return Json(new { data = Companys });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var Company = _unitOfWork.CompanyRepository.Get(u => u.Id == id);
            if (Company == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            //var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, Company.ImageUrl.TrimStart('\\'));
            //if (System.IO.File.Exists(oldImagePath))
            //{
            //    System.IO.File.Delete(oldImagePath);
            //}

            _unitOfWork.CompanyRepository.Remove(Company);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleting successful" });
        }
        #endregion
    }
}
