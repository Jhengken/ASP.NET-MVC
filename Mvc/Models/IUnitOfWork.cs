namespace Mvc.Models;

public interface IUnitOfWork
{
    DbContext Context { get; set; }
    Task CommitAsync();
    string ConnectionString { get; set; }
}