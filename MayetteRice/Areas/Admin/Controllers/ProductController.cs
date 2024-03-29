using MayetteRice.DataAccess.Data;
using MayetteRice.DataAccess.Repository.IRepository;
using MayetteRice.Models;
using MayetteRice.Models.ViewModels;
using MayetteRice.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MayetteRice.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        // Dependency Injection
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();
            return View(objProductList);
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new() 
            { 
                CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };

            if (id == null || id == 0)
            {
                // Creation of product
                return View(productVM);
            }
            else
            {
                // Updating a product
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null) 
                { 
                    // Upload the file and save in the product folder under images
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    if(!string.IsNullOrEmpty(productVM.Product.ImageUrl)) 
                    { 
                        // There is an image url that is already present
                        // In this case, we have to delete the old image 
                        var oldImagePath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));

                        if (System.IO.File.Exists(oldImagePath)) 
                        { 
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    // This is used to upload a new image
                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create)) 
                    {
                        file.CopyTo(fileStream);
                    }

                    // This is used to update the image url
                    productVM.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(productVM.Product.Id == 0) 
                {
                    // Creating a new product entry - ADD
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else 
                { 
                    // Updating a product entry
                    _unitOfWork.Product.Update(productVM.Product);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product created successfully!";
                return RedirectToAction("Index");
            }
            else 
            {
                productVM.CategoryList = _unitOfWork.Category
                .GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(productVM);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll(includeProperties: "Category").ToList();
            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);

            if(productToBeDeleted == null) 
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }

            var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, productToBeDeleted.ImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
