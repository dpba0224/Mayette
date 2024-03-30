using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MayetteRice.DataAccess.DbInitializer
{
    public interface IDbInitializer
    {
        // This method is responsible for creating admin user and roles of the website
        void Initialize();
    }
}
