// 保哥寫的套件
// dotnet new --install Duotify.Templates.DotNetNew
// dotnet new efr

namespace Mvc.Models;

public class CourseRepository : EfRepository<Course>, ICourseRepository
{
    // 開放封閉原則，直接封裝在這裡，需要在擴充，且不會被外部修改
    // Controller 盡量不放商業邏輯，所以在這邊使用 where 條件
    public List<Course> FindCreditsThen3()
    {
        // 用 this
        return this
            .Where(w => w.Credits > 3)
            .ToList();
    }

    public override IQueryable<Course> All()
    {
        // 可以在這裡做一層篩選軟刪除的功能
        // 用 base
        return base
            .All()
            .Where(w => w.IsDeleted == false);
    }
}

public interface ICourseRepository : IRepository<Course>
{
    public List<Course> FindCreditsThen3();
}