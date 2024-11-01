using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazor.Models;

namespace WebAppRazor.Pages.Categories
{
    [BindProperties]
    public class UpdateModel : PageModel
    {
        private ApplicationDbContext _db;
        public UpdateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category Categories { get; set; }
        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
                Categories = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(Categories);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToPage("Index", "Categories");
            }
            return Page();
        }
    }
}
