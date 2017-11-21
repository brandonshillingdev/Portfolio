using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public interface iRepository<T> where T : class
    {
        //repository
        //add update and delete functions

        IQueryable<T> GetALL();
        void Add(T entity);
        void Delete(T entity);
    }
}
