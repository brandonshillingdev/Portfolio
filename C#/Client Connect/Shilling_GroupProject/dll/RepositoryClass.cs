using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbOps2
{
    public class RepositoryClass<T> : iRepository<T> where T : class
    {
        public RepositoryClass(DbContext dbcontext)
        {
            //check if null
            if (dbcontext == null)
            {
                throw new ArgumentException("Null");
            }
            //set DbContext
            DbContext = dbcontext;
            //set DbSet
            DbSet = DbContext.Set<T>();
        }
        //getters and setters
        protected DbContext DbContext { get; set;}
        protected DbSet<T> DbSet { get; set; }
        

        public void Add(T entity)
        {
            ///add new entity
            //create new entity object and set equal to current entity
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);

            //checks to see if entity state is detached
            if (dbEntityEntry.State != EntityState.Detached)
            {
                // if not detached then set entity state to added
                dbEntityEntry.State = EntityState.Added;
            }
            else
            {
                //adds entity
                DbSet.Add(entity);
            }
        }

        public void Delete(T entity)
        {
            ///Deletes entity
            //create new entity object and set equal to current entity
            DbEntityEntry dbEntityEntry = DbContext.Entry(entity);

            //checks to see if entity state is detached
            if (dbEntityEntry.State != EntityState.Deleted)
            {
                // if not detached then set entity state to deleted
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                //deletes entity
                DbSet.Attach(entity);
                DbSet.Remove(entity);
            }
        }

        public virtual void Delete(int id)
        {
            //deletes entity by id
            T entity = GetById(id);
            //if null return
            if(entity == null)
            {
                return;
            }
            //calls Delete Function
            Delete(entity);
        }


        public IQueryable<T> GetALL()
        {
            //returns entity
            return DbSet;
        }

        public virtual T GetById(int id)
        {
            //returns the id
            return DbSet.Find(id);
        }
    }
}
