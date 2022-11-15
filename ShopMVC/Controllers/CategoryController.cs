using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository db;
        public CategoryController(ICategoryRepository db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = db.GetAll().ToList();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Add(category);
                db.Save();

                TempData[WebConstants.Success] = "Category created successfully";

                return RedirectToAction(nameof(Index));
            }

            TempData[WebConstants.Error] = "Error while creating category";

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var category = db.FindById(id.GetValueOrDefault());

            if (category == null)
                return NotFound();
            
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Update(category);
                db.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var category = db.FindById(id.GetValueOrDefault());

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int? id)
        {
            var category = db.FindById(id.GetValueOrDefault());
                
            if (category == null)
                return NotFound();

            db.Remove(category);
            db.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
