using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace CapaNavDoc.Models.BusinessLayers
{
    public class BusinessLayer<T> where T : class
    {
        private readonly DbContext _dbContext;


        public BusinessLayer(DbContext context)
        {
            _dbContext = context;
        }


        public T Insert(T item)
        {
            _dbContext.Set<T>().Add(item);
            _dbContext.SaveChanges();

            return item;
        }

        public T Update(T item)
        {
            _dbContext.Entry(item).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return item;
        }

        public void Delete(T item)
        {
            _dbContext.Entry(item).State = EntityState.Deleted;
            _dbContext.SaveChanges();
        }


        public List<T> GetList()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T Get(int id)
        {
            return _dbContext.Set<T>().Find(id);
        }

        public void Delete(int id)
        {
            Delete(Get(id));
        }

    }
}