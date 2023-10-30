using Microsoft.AspNetCore.Mvc.Routing;
using SupplyChain.App.Notification;
using SupplyChain.App.Profiles;
using SupplyChain.App.Utils;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.App.Utils.Validations;
using SupplyChain.Infrastructure;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

//using session state
// Using the GetValue<type>(string key) method
double sessionTimeout = builder.Configuration.GetValue<double>("SessionTimeOut");
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeout); // Adjust as needed
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.Cookie.Name = $"{Guid.NewGuid()}"; // A unique name for your session cookie
    options.Cookie.IsEssential = true; // Helps maintain the session even with limited cookie support
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.MaxAge = TimeSpan.FromHours(1); // Adjust this as needed
    options.Cookie.Expiration = TimeSpan.FromMinutes(30); // Adjust as needed
});

//Infrastructure Services
builder.Services.InfrastructureServices(builder.Configuration);

#region Mappers
// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IManufacturerService, ManufacturerService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IRolePermissionService, RolePermissionService>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventStatusService, EventStatusService>();
builder.Services.AddScoped<IProductEventService, ProductEventService>();
builder.Services.AddScoped<IProductQuantityRequestService, ProductQuantityRequestService>();
builder.Services.AddSingleton<IUrlHelperFactory, UrlHelperFactory>();

builder.Services.AddHttpContextAccessor();

#endregion

#region Attributes
builder.Services.AddScoped<SessionExpireAttribute>();
builder.Services.AddScoped<NoCacheAttribute>();
builder.Services.AddScoped<FutureDateAttribute>();
builder.Services.AddScoped<CustomPhoneAttribute>();
#endregion

#region Utils
builder.Services.AddScoped<IUploadFile, UploadFile>();
builder.Services.AddScoped<ILookUp, Lookups>();
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true; // Enable detailed error messages for debugging
    // Other SignalR options
});
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseSession();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapHub<NotificationHub>("/notificationHub");

//app.UseAuthenticationMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
    );

app.Run();
