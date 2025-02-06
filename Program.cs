using Cl.DataAccess.EF.Repository;
using Cl.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using TestDev.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<ResponeActionResult>();
builder.Services.AddScoped<IUnitOfWork, EFUnitOfWork>();
builder.Services.AddScoped<SanPhamRepository>();
builder.Services.AddScoped<LoaiSanPhamRepository>();
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {

        //options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.IgnoreNullValues = true;
        // Hoặc options.JsonSerializerOptions.DictionaryKeyPolicy = null; nếu cần với Dictionary
    });


var app = builder.Build();
//using (var scope = app.Services.CreateScope())
//{
//    var dbContext = scope.ServiceProvider.GetRequiredService<DBContext>();
//    dbContext.SeedData();
//}

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
    pattern: "{controller=SanPham}/{action=Index}/{id?}");

app.Run();
