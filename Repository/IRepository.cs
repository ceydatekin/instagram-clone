using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Instagram.InstagramContext;
using Instagram.Manager;

namespace Instagram.Repository
{
    public abstract class IRepository<T> where T : class
    {
        instagramContext context = ContextManager.GetContext();


        public T GetById(Expression<Func<T, bool>> predicate)
        {
            return context.Set<T>().SingleOrDefault(predicate);
        }
        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T Insert(T entity)
        {

            context.Set<T>().Add(entity);
            Save();
            return entity;

        }
        public void Update(T entity)
        {

        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            Save();
        }
        public void Save()
        {
            context.SaveChanges();
        }
       
    }
}
