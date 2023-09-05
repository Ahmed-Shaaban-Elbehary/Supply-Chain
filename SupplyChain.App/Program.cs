using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;
using SupplyChain.App.App_Class;
using SupplyChain.App.Notification;
using SupplyChain.App.Profiles;
using SupplyChain.App.Utils;
using SupplyChain.App.Utils.Contracts;
using SupplyChain.Infrastructure;
using SupplyChain.Services;
using SupplyChain.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

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
#endregion

#region Utils
builder.Services.AddScoped<IUploadFile, UploadFile>();
builder.Services.AddScoped<ILookUp, Lookups>();
builder.Services.AddSignalR();
string connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddTransient<SqlDependency>(_ => new SqlDependency(new SqlConnection(connString)));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.UseAuthenticationMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}"
    );

app.MapHub<NotificationHub>("/Notification");

app.Run();
