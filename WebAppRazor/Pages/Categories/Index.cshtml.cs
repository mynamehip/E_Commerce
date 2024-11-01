using E_Commerce.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppRazor.Models;

namespace WebAppRazor.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _db.Categories.ToList();
        }
    }
}
