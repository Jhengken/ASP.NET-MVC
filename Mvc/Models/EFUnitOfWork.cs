namespace Mvc.Models;

public class EfUnitOfWork(ContosoUniversityContext context) : IUnitOfWork
{
    public DbContext Context { get; set; } = context;

    public Task CommitAsync()
    {
        return Context.SaveChangesAsync();
    }

    public string ConnectionString
    {
        get => Context.Database.GetConnectionString()!;
        set => Context.Database.SetConnectionString(value);
    }
}