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
    public class CompanyController : Controller
    {
        // Dependency Injection
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return View(objCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                // Creation of company
                return View(new Company());
            }
            else
            {
                // Updating a company
                Company companyObj = _unitOfWork.Company.Get(u => u.Id == id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            { 
                if(companyObj.Id == 0) 
                {
                    // Creating a new company entry - ADD
                    _unitOfWork.Company.Add(companyObj);
                }
                else 
                { 
                    // Updating a company entry
                    _unitOfWork.Company.Update(companyObj);
                }

                _unitOfWork.Save();
                TempData["success"] = "Company created successfully!";
                return RedirectToAction("Index");
            }
            else 
            {
                return View(companyObj);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll() 
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted = _unitOfWork.Company.Get(u => u.Id == id);

            if(companyToBeDeleted == null) 
            {
                return Json(new
                {
                    success = false,
                    message = "Error while deleting"
                });
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
