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

builder.Services.AddScoped<IProductMapper, ProductMapper>();

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IUploadFile, UploadFile>();

builder.Services.AddSingleton<ILookUp, Lookups>();

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
