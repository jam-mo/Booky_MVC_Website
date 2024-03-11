using BookWeb.Data;
using BookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // retrieve all categories from db 
            List<Category> objCategoryList = _db.Categories.ToList();
            // pass categories to view so it can be used in data
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
