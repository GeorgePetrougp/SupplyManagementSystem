using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using SupplyMS.Plugins.EFCore;
using SupplyMS.UseCases;
using SupplyMS.UseCases.Interfaces;
using SupplyMS.UseCases.Inventories;
using SupplyMS.UseCases.PluginInterfaces;
using SupplyMS.UseCases.Products;
using SupplyWebApp.Areas.Identity;
using SupplyWebApp.Data;

namespace SupplyWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            builder.Services.AddSingleton<WeatherForecastService>();

            builder.Services.AddDbContext<DataContext>(opt =>
            {
                opt.UseInMemoryDatabase("SupplyDb");
            });
            //Dependency Injection Repositories
            builder.Services.AddTransient<IInventoryRepository, InventoryRepository>();
            builder.Services.AddTransient<IProductRepository, ProductRepository>();
            builder.Services.AddTransient<IInventoryTrasactionRepository, InventoryTrasactionRepository>();
            builder.Services.AddTransient<IProductTransactionRepository, ProductTransactionRepository>();

            //Dependency Injection Cases
            builder.Services.AddTransient<IViewInventoriesByNameUserCase, ViewInventoriesByNameUserCase>();
            builder.Services.AddTransient<IAddInventoryUseCase, AddInventoryUseCase>();
            builder.Services.AddTransient<IEditInventoryUseCase, EditInventoryUseCase>();
            builder.Services.AddTransient<IViewInventoryByIdUseCase, ViewInventoryByIdUseCase>();
            builder.Services.AddTransient<IViewProductsByNameUserCase, ViewProductsByNameUserCase>();
            builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
            builder.Services.AddTransient<IViewProductByIdUseCase, ViewProductByIdUseCase>();
            builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
            builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
            builder.Services.AddTransient<IPurchaseInventoryUseCase, PurchaseInventoryUseCase>();
            builder.Services.AddTransient<IValidateInventoriesForProducingUseCase, ValidateInventoriesForProducingUseCase>();
            builder.Services.AddTransient<IProduceProductUseCase, ProduceProductUseCase>();



            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var supplyContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            supplyContext.Database.EnsureDeleted();
            supplyContext.Database.EnsureCreated();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}