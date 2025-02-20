using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mvc.ActionFilter;
using Mvc.ValidationAttribute;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.TryAddScoped<判斷有無重複的課程名稱Attribute>();
builder.Services.TryAddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.TryAddScoped<ICourseRepository, CourseRepository>();

// 掛單一功能 或 Controller 的 Attribute 需要用的方式
// builder.Services.AddScoped<HttpDurationActionFilter>();

// 掛全域的 ActionFilter
builder.Services.AddMvc(options => { options.Filters.Add<HttpDurationActionFilter>(); });

builder.Services.AddDbContext<ContosoUniversityContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();