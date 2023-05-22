using SupplyChain.App.App_Class;
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

/********* Dependencies Container ***************/
builder.Services.AddScoped<DependencyContainer>();
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
