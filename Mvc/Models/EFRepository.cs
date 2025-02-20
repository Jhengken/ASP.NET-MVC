namespace Mvc.Models;

public class EfRepository<T> : IRepository<T> where T : class
{
    public required IUnitOfWork UnitOfWork { get; set; }

    private DbSet<T>? _objectset;
    private DbSet<T>? ObjectSet => _objectset ??= UnitOfWork.Context.Set<T>();

    public virtual IQueryable<T> All()
    {
        return ObjectSet!.AsQueryable();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> expression)
    {
        return ObjectSet!.Where(expression);
    }

    public virtual void Add(T entity)
    {
        ObjectSet!.Add(entity);
    }

    public virtual void Delete(T entity)
    {
        ObjectSet!.Remove(entity);
    }
    public virtual void Update(T entity)
    {
        ObjectSet!.Update(entity);
    }
    public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> func)
    {
        return ObjectSet!.AnyAsync(func);
    }
}