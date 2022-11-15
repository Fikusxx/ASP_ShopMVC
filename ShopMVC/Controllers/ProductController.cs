using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopMVC_DataAccess;
using ShopMVC_Models;
using ShopMVC_Utility;
using ShopMVC_ViewModels;

namespace ShopMVC.Controllers
{
    [Authorize(Roles = WebConstants.AdminRole)]
    public class ProductController : Controller
    {
        private readonly IProductRepository db;
        private readonly IWebHostEnvironment hostingEnvironment;
        public ProductController(IProductRepository db, IWebHostEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var includeProperties = new List<string>()
            {
                WebConstants.CategoryName,
                WebConstants.ApplicationTypeName
            };

            var products = db.GetAll(includeProperties: includeProperties).ToList();

            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var categoriesSelectList = db.GetSelectList(WebConstants.CategoryName);
            var appTypesSelectList = db.GetSelectList(WebConstants.ApplicationTypeName);

            var model = new CreateProductViewModel()
            {
                CategorySelectList = categoriesSelectList,
                ApplicationTypesSelectList = appTypesSelectList,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = await FileHandler.ProcessUploadedFile(model.Image, hostingEnvironment);

                var product = new Product()
                {
                    Name = model.Name,
                    Price = model.Price,
                    Description = model.Description,
                    ShortDescription = model.ShortDescription,
                    PhotoPath = uniqueFileName,
                    CategoryId = model.CategoryId,
                    ApplicationTypeId = model.ApplicationTypeId
                };

                db.Add(product);
                db.Save();

                return RedirectToAction(nameof(Index));
            }

            var categoriesSelectList = db.GetSelectList(WebConstants.CategoryName);
            var appTypesSelectList = db.GetSelectList(WebConstants.ApplicationTypeName);

            model.CategorySelectList = categoriesSelectList;
            model.ApplicationTypesSelectList = appTypesSelectList;

            return View(model);
        }


        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var product = db.FindById(id.GetValueOrDefault());

            if (product == null)
                return NotFound();

            var model = new EditProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                CategoryId = product.CategoryId,
                ApplicationTypeId = product.ApplicationTypeId,
                PhotoPath = product.PhotoPath,
            };

            var categoriesSelectList = db.GetSelectList(WebConstants.CategoryName);
            var appTypesSelectList = db.GetSelectList(WebConstants.ApplicationTypeName);

            model.CategorySelectList = categoriesSelectList;
            model.ApplicationTypesSelectList = appTypesSelectList;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var product = db.FindById(model.Id);

                if (product == null)
                    return NotFound();

                product.Name = model.Name;
                product.Price = model.Price;
                product.Description = model.Description;
                product.ShortDescription = model.ShortDescription;
                product.CategoryId = model.CategoryId;
                product.ApplicationTypeId = model.ApplicationTypeId;
                product.PhotoPath = model.PhotoPath;

                if (model.Image != null) // if we uploaded a new photo
                {
                    if (model.PhotoPath != null) // if product already had a photo
                    {
                        // delete photo product has
                        var filePath = Path.Combine(hostingEnvironment.WebRootPath, WebConstants.ImagePath, model.PhotoPath);

                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                    }

                    product.PhotoPath = await FileHandler.ProcessUploadedFile(model.Image, hostingEnvironment);
                }

                db.Update(product);
                db.Save();

                return RedirectToAction(nameof(Index));
            }

            var categoriesSelectList = db.GetSelectList(WebConstants.CategoryName);
            var appTypesSelectList = db.GetSelectList(WebConstants.ApplicationTypeName);

            model.CategorySelectList = categoriesSelectList;
            model.ApplicationTypesSelectList = appTypesSelectList;

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var product = db.FindById(id.GetValueOrDefault());

            if (product == null)
                return NotFound();

            var model = new DeleteProductViewModel()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ShortDescription = product.ShortDescription,
                CategoryId = product.CategoryId,
                ApplicationTypeId = product.ApplicationTypeId,
                PhotoPath = product.PhotoPath
            };

            var categoriesSelectList = db.GetSelectList(WebConstants.CategoryName);
            var appTypesSelectList = db.GetSelectList(WebConstants.ApplicationTypeName);

            model.CategorySelectList = categoriesSelectList;
            model.ApplicationTypesSelectList = appTypesSelectList;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(DeleteProductViewModel model)
        {
            var product = db.FindById(model.Id);

            if (product == null)
                return NotFound();

            // delete photo product has
            if (product.PhotoPath != null)
            {
                var filePath = Path.Combine(hostingEnvironment.WebRootPath, WebConstants.ImagePath, product.PhotoPath);

                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);
            }

            db.Remove(product);
            db.Save();

            return RedirectToAction(nameof(Index));
        }
    }
}
