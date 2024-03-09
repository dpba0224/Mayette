using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MayetteRice.DataAccess.Data;
using MayetteRice.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MayetteRice.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        // Dependency Injection
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();

            // This is used so that Category will automatically be populated when it retrieves all the product based on the foreign key relation
            _db.Products.Include(u => u.Category);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.FirstOrDefault();
        }

        // Basically if someone gives a Category or CoverType (Category, CoverType), we can build to include these properties
        public IEnumerable<T> GetAll(string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if(!string.IsNullOrEmpty(includeProperties)) 
            { 
                foreach(var includeProp in includeProperties
                    .Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries)) 
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}
