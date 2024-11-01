using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazor.Models;

namespace WebAppRazor.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _db;
        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public Category Categories { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _db.Add(Categories);
            _db.SaveChanges();
            TempData["success"] = "Category created successfully";
            return RedirectToPage("Index", "Categories");
        }
    }
}
