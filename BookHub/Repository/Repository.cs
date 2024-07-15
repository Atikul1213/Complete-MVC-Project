using BookHub.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookHub.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private readonly ApplicationDbContext _db;
        private readonly DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
           _db.SaveChanges();
             
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            
            return query.FirstOrDefault();
        }

        public IEnumerable<T> Getall()
        {
            IQueryable<T> query = dbSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
            _db.SaveChanges();
        }
    }
}
