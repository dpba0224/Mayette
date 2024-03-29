using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MayetteRice.DataAccess.Repository.IRepository
{
    // This is a generic interface for all models present in the project
    public interface IRepository<T> where T : class
    {
        // T - Category or any generic model present in the project

        // This retrieves all items or entities present in a model
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);

        // This retrieves only one item or entity from the model
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);

        // Methods for entities or items present in the model
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
