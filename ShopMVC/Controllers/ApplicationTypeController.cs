using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository db;
        public ApplicationTypeController(IApplicationTypeRepository db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var appTypes = db.GetAll().ToList();
            return View(appTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType appType)
        {
            if (ModelState.IsValid)
            {
                db.Add(appType);
                db.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(appType);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var appType = db.FindById(id.GetValueOrDefault());

            if (appType == null)
                return NotFound();

            return View(appType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType type)
        {
            if (ModelState.IsValid)
            {
                db.Update(type);
                db.Save();

                return RedirectToAction(nameof(Index));
            }

            return View(type);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var appType = db.FindById(id.GetValueOrDefault());

            if (appType == null)
                return NotFound();

            return View(appType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int? id)
        {
            var appType = db.FindById(id.GetValueOrDefault());

            if (appType == null)
                return NotFound();

            db.Remove(appType);
            db.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
