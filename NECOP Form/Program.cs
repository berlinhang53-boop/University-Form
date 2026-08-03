using Microsoft.EntityFrameworkCore;
using NECOP_Form.Data;
using NECOP_Form.Models;


var builder = WebApplication.CreateBuilder(args);


//ye sql server k lie

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));



builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});




// ye postgree sql use karny k lie

//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseNpgsql(
//        builder.Configuration.GetConnectionString("DefaultConnection")
//    ));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();



//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();
//}











// ye line add krni hai deployment k lie


//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();
//}








//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    db.Database.Migrate();

//    // Seed Designation dropdown list
//    if (!db.DgModels.Any())
//    {
//        db.DgModels.AddRange(
//            new DgModel { Name = "Manager" },
//            new DgModel { Name = "Developer" },
//            new DgModel { Name = "Analyst" },

//               new DgModel { Name = "BPS 21" },
//            new DgModel { Name = "HR SPECIALIST" },
//            new DgModel { Name = "SPS 10" },
//             new DgModel { Name = "SPS 8" }



//        );
//    }

//    // Seed Department dropdown list
//    if (!db.Departments.Any())
//    {
//        db.Departments.AddRange(
//            new DepartmentModel { DepartmentName = "IT" },
//            new DepartmentModel { DepartmentName = "HR" },
//            new DepartmentModel { DepartmentName = "Finance" },
//            new DepartmentModel { DepartmentName = "Power Supply" },
//            new DepartmentModel { DepartmentName = "Electronics" },
//            new DepartmentModel { DepartmentName = "Education" },
//               new DepartmentModel { DepartmentName = "Robotics" }

//        );
//    }

//    db.SaveChanges();
//}





// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();

app.UseStaticFiles();  //UseStaticFiles() runtime par wwwroot folder ke andar ki saari files ko dynamically serve karta hai — chahe wo build time pe thi ya baad mein upload hui hon.

app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
       //pattern: "{controller=Home}/{action=Index}/{id?}")
       pattern: "{controller=Designation}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();  