namespace Mvc.Models;

public interface IRepository<T>
{
    IUnitOfWork UnitOfWork { get; set; }
    IQueryable<T> All();
    IQueryable<T> Where(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void Delete(T entity);
    void Update(T entity);
}