using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazor.Models;

namespace WebAppRazor.Pages.Categories
{
    [BindProperties]
    public class RemoveModel : PageModel
    {
        private ApplicationDbContext _db;
        public RemoveModel(ApplicationDbContext db)
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
            _db.Categories.Remove(Categories);
            _db.SaveChanges();
            TempData["success"] = "Category removed successfully";
            return RedirectToPage("Index", "Categories");
        }
    }
}
