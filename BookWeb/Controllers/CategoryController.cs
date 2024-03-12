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
        [HttpPost] // when something is posted, this is  called
        public IActionResult Create(Category category)
        {
           
            if (category.Name != null && category.Name.ToLower() == "test")
            {
                ModelState.AddModelError("", "Test is an invalid valye");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(category);
                _db.SaveChanges();
                TempData["Success"] = "Category Created!";
                return RedirectToAction("Index", "Category");
            }
            // Once saved, returns to Cat index with updated tab;e}

            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category retrieveCategory = _db.Categories.Find(id);
            if (retrieveCategory == null)
            {
                return NotFound();
            }
            return View(retrieveCategory);
        }
        [HttpPost] 
        public IActionResult Edit(Category category)
        {

            
            if (ModelState.IsValid)
            {
                _db.Categories.Update(category);
                _db.SaveChanges();
                TempData["Success"] = "Category Updated!";
                return RedirectToAction("Index", "Category");
            }
            

            return View();
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category retrieveCategory = _db.Categories.Find(id);
            if (retrieveCategory == null)
            {
                return NotFound();
            }
            return View(retrieveCategory);
        }
        [HttpPost, ActionName("Delete")] // Gives a different name since name and param identical, to differentiate
        public IActionResult DeletePost(int? id)
        {

            Category? category = _db.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            TempData["Success"] = "Category Deleted!";
            return RedirectToAction("Index", "Category");
        }

    }
}
