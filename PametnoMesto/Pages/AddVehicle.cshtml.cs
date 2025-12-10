using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class AddVehicleModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public AddVehicleModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        [BindProperty]
        public string Name { get; set; } = "";

        [BindProperty]
        public string Color { get; set; } = "";

        [BindProperty]
        public VehicleType Type { get; set; } = VehicleType.Car;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _dataHandler.AddVehicle(Name, Color, Type);
            return RedirectToPage("/Vehicles");
        }
    }
}