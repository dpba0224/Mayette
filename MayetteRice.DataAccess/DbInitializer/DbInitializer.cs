﻿using MayetteRice.DataAccess.Data;
using MayetteRice.Models;
using MayetteRice.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayetteRice.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        // Dependency Injection
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;
        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db){ 
                _roleManager = roleManager;
                _userManager = userManager;
                _db = db;
            }

        public void Initialize()
        {
            // Migrations if they are not applied
            try 
            {
                if (_db.Database.GetPendingMigrations().Count() > 0) 
                { 
                    _db.Database.Migrate();
                }
            }
            catch(Exception ex) 
            { 
            
            }

            // Create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Customer).GetAwaiter().GetResult()) {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Customer)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Company)).GetAwaiter().GetResult();


                // If roles are not created, then we will create admin user as well
                _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = "admin@mayetterice.com",
                    Email = "admin@mayetterice.com",
                    Name = "Marietta Abrugena",
                    PhoneNumber = "09564716784",
                    StreetAddress = "B1 L19, Ciudad Grande Subdivision",
                    Barangay = "Market Area",
                    City = "Santa Rosa",
                    Province = "Laguna",
                    PostalCode = "4026"
                }, "Jufroner_40!").GetAwaiter().GetResult();

                // Used to retrieve the user object to the database
                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@mayetterice.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
