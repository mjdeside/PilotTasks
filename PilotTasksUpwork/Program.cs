using PilotTasksUpworkService.DatabaseAccess;
using PilotTasksUpworkService.Repository;
using PilotTasksUpworkService.Repository.Interface;
using PilotTasksUpworkService.Services;
using PilotTasksUpworkService.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDatabaseContext, DatabaseContext>();

//Repository
builder.Services.AddTransient<IProfileRepository, ProfileRepository>();
builder.Services.AddTransient<ITasksRepository, TasksRepository>();


//Services
builder.Services.AddTransient<IProfileService, ProfileService>();
builder.Services.AddTransient<ITasksService, TasksService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
