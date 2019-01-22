using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private aaaEntities Context;

        public BaseRepository()
        {
            // this constructor is automatically invoked when the default child constructor is called
            Context = new aaaEntities();
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }
        public T GetByID(int id)
        {
            return Context.Set<T>().Find(id);
        }
        public void Create(T item)
        {
            Context.Set<T>().Add(item);
            Context.SaveChanges();
        }
        public void Update(T item, Func<T, bool> findByIDPredecate)
        {
            var local = Context.Set<T>()
                         .Local
                         .FirstOrDefault(findByIDPredecate);// (f => f.ID == item.ID);
            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            Context.Entry(item).State = EntityState.Modified;

        
            Context.SaveChanges();
        }
        public bool DeleteByID(int id)
        {
            bool isDeleted = false;
            T dbItem = Context.Set<T>().Find(id);
            if (dbItem != null)
            {
                Context.Set<T>().Remove(dbItem);
                int recordsChanged = Context.SaveChanges();
                isDeleted = recordsChanged > 0;
            }
            return isDeleted;
        }

        // This method has to be written in all inhereted classes
        public abstract void Save(T item);
    }
}
