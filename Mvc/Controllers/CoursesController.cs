using Microsoft.AspNetCore.Mvc;
using Mvc.ViewModels;
using X.PagedList.Extensions;

namespace Mvc.Controllers;

// [ServiceFilter<HttpDurationActionFilter>] // 有 DI logger
// [HttpDurationActionFilter] // 沒有 DI logger
[Route("teach/course")]
public class CoursesController : Controller
{
    private readonly ICourseRepository _repo;

    public CoursesController(ICourseRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _repo.UnitOfWork = uow;
    }

    [HttpGet("index")]
    public IActionResult Index(int page = 1)
    {
        // Controller 盡量不放商業邏輯，所以少在這邊使用 where 條件
        // var findCreditsThen3 = _repo.FindCreditsThen3();

        var course = _repo.All()
            .AsQueryable();

        // 明確轉型
        // var list = course.Select(s=>(CourseList)s).ToList();
        //隱含轉型
        // CourseList t = course.First();

        var pagedList = course.ToPagedList(page, 3);

        ViewBag.OnePageOfProducts = pagedList;

        return View(pagedList);
    }

    //可以增加規則，如限定詞
    //超過會給404
    // [HttpGet("info/{slug:int=1231}")] // 可以給預設值
    // [HttpGet("info/{slug:alpha}")]    // 比對大寫或小寫拉丁字母，也可以用正則
    // [HttpGet("info/{slug:int?}")]     // 可以給null，同時簽章也可以給預設值
    [HttpGet("info/{slug:maxlength(5)}")]
    public async Task<IActionResult> Details(string slug)
    {
        var course = await _repo.All()
            .FirstOrDefaultAsync(m => m.Slug == slug);

        if (course == null)
        {
            return NotFound();
        }

        course.Title = course.Slug;

        return View(course);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        // ViewData["CourseId"] = new SelectList(repo.Departments, "DepartmentId", "DepartmentId");
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CourseCreate courseCreate)
    {
        if (ModelState.IsValid)
        {
            var course = new Course
            {
                Title = courseCreate.Title,
                Credits = courseCreate.Credits
            };
            _repo.Add(course);
            await _repo.UnitOfWork.CommitAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(courseCreate);
    }

    [HttpGet("edit")]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _repo
            .Where(w => w.CourseId == id)
            .FirstOrDefaultAsync();

        if (course == null)
        {
            return NotFound();
        }

        // ViewData["CourseId"] = new SelectList(_repo.Departments, "DepartmentId", "DepartmentId", course.CourseId);
        return View(course);
    }

    [HttpPost("edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Course course)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _repo.Update(course);
                await _repo.UnitOfWork.CommitAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CourseExists(course.CourseId))
                {
                    return NotFound();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // ViewData["CourseId"] = new SelectList(_repo.Departments, "DepartmentId", "DepartmentId", course.CourseId);
        return View(course);
    }

    [HttpGet("delete")]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var course = await _repo.All()
            .Include(c => c.Department)
            .FirstOrDefaultAsync(m => m.CourseId == id);

        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    [HttpPost("delete"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var course = await _repo
            .All()
            .FirstOrDefaultAsync(f => f.CourseId == id);

        if (course != null)
        {
            _repo.Delete(course);
        }

        await _repo.UnitOfWork.CommitAsync();
        return RedirectToAction(nameof(Index));
    }

    private Task<bool> CourseExists(int id)
    {
        return _repo.All().AnyAsync(a => a.CourseId == id);
    }
}