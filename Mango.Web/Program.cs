using Mango.Web.Services;
using Mango.Web.Services.IService;
using Mango.Web.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register httpClientFactory for dependency injection
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Coupon Service will use HttpClient
builder.Services.AddHttpClient<ICouponService,CouponService>();

// Configure APIBase from appsettings.json
SD.CouponAPIBase = builder.Configuration["ServiceURLs:CouponAPI"];
SD.AuthAPIBase = builder.Configuration["ServiceURLs:AuthAPI"];

// Dependency Injection for BaseService and CouponService
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ICouponService, CouponService>();
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
