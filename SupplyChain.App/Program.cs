using SupplyChain.App.Mappers;
using SupplyChain.App.Mappers.Contracts;
using SupplyChain.App.Profiles;
using SupplyChain.App.Profiles.Contracts;
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
builder.Services.AddScoped<IProductMapper, ProductMapper>();
builder.Services.AddScoped<IProductCategoryMapper, ProductCategoryMapper>();
#endregion

#region Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
#endregion

#region Utils
builder.Services.AddScoped<IUploadFile, UploadFile>();
builder.Services.AddScoped<ILookUp, Lookups>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
