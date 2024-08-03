using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting.Internal;
using Mvc21BITV01MidTest.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<MyeStoreContext>(opstions =>
{
    opstions.UseSqlServer(builder.Configuration.GetConnectionString("PKhanh"));
});


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

app.MapControllerRoute(
    name: "Chi Tiet Hoa Don",
    pattern: "ChiTietHds/{MaCt}",
    defaults: new { controller = "ChiTietHds", action = "Details" }
    );

app.Run();
