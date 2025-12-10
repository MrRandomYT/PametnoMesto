using PametnoMesto.Scripts;
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

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
