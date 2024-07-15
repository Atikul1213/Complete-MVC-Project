using System.Linq.Expressions;

namespace BookHub.Repository
{
    public interface IRepository<T> where T : class
    {

        IEnumerable<T> Getall();

        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        void Remove(T entity);

        void Update(T entity);
    }
}
