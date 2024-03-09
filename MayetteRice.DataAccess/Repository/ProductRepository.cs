using MayetteRice.DataAccess.Data;
using MayetteRice.DataAccess.Repository.IRepository;
using MayetteRice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MayetteRice.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if(objFromDb != null) 
            { 
                // MANUAL MAPPING
                objFromDb.Title = obj.Title;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.DiscPrice = obj.DiscPrice;
                objFromDb.CategoryId = obj.CategoryId;
                
                if(obj.ImageUrl != null) 
                {
                    objFromDb.ImageUrl = obj.ImageUrl;                
                }
            }
        }
    }
}
