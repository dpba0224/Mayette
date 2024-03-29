﻿using MayetteRice.DataAccess.Repository.IRepository;
using MayetteRice.Utility;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MayetteRice.ViewComponents
{
    // This will serve as the back-end file for the view component
    public class ShoppingCartViewComponent : ViewComponent
    {
        // Dependency Injection
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync() 
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionCart) == null) 
                {
                    HttpContext.Session.SetInt32(SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).Count());
                }

                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else 
            {
                HttpContext.Session.Clear();
                return View(0);
            }
        }
    }
}
