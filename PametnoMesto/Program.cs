using PametnoMesto.Scripts;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
namespace PametnoMesto
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            
            var handler = new DataHandler();
            handler.AddVehicle("Tesla Model 3", "White", VehicleType.Car);
            handler.AddVehicle("City Scooter X", "Blue", VehicleType.Scooter);
            handler.AddVehicle("Urban Bike", "Red", VehicleType.Bike);
            handler.AddVehicle("Smart Bus", "Yellow", VehicleType.Car);
            builder.Services.AddSingleton(handler);

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login"; // Kam gre uporabnik, če ni prijavljen
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // Koliko časa traja prijava
                });

            // Parse Error Fix
            var cultureInfo = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
