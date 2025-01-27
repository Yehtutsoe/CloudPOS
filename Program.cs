using CloudPOS.DAO;
using CloudPOS.Repositories;
using CloudPOS.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache(); 
builder.Services.AddSession(options => 
{   options.IdleTimeout = TimeSpan.FromMinutes(30); 
    options.Cookie.HttpOnly = true; 
    options.Cookie.IsEssential = true;
});
var config = builder.Configuration;   //declare the configure to read json
//add the dbContext that we defined the ApplicationDbContext to get connestion string
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(config.GetConnectionString("CloudPOSConnectionString")));
//Register Identity for UIs
builder.Services.AddRazorPages();
builder.Services.AddTransient<IPhoneModelService, PhoneModelService>();
builder.Services.AddScoped<IPhoneModelRepository, PhoneModelRepository>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository,ProductRepository>();
builder.Services.AddScoped<ISupplierRepository, SupplierRepository>();
builder.Services.AddTransient<ISupplierService, SupplierService>();
builder.Services.AddTransient<IStockIncomeService, StockIncomeService>();
builder.Services.AddScoped<IStockIncomeRepository, StockIncomeRepository>();
builder.Services.AddTransient<ISaleProcessService, SaleProcessService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddTransient<ISaleItemService, SaleItemService>();
builder.Services.AddScoped<ISaleItemRepository, SaleItemRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();


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
app.UseSession(); // for AddToCart Function
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
//Mapping the razor page route
app.MapRazorPages();
app.Run();
