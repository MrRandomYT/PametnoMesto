using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class EditVehicleModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public EditVehicleModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        [BindProperty]
        public int VehicleId { get; set; }

        [BindProperty]
        public string Name { get; set; } = "";

        [BindProperty]
        public string Color { get; set; } = "";

        [BindProperty]
        public VehicleType Type { get; set; } = VehicleType.Car;

        [BindProperty]
        public bool IsAvailable { get; set; }

        public IActionResult OnGet(int id)
        {
            var vehicle = _dataHandler.GetVehicle(id); // You’ll need a GetVehicle method
            if (vehicle == null)
                return RedirectToPage("/Vehicles");

            VehicleId = vehicle.Id;
            Name = vehicle.Name;
            Color = vehicle.Color;
            Type = vehicle.Type;
            IsAvailable = vehicle.IsAvailable;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            bool success = _dataHandler.UpdateVehicle(VehicleId, Name, Color, Type, IsAvailable);
            if (!success)
                ModelState.AddModelError("", "Vehicle not found.");

            return RedirectToPage("/Vehicles");
        }
    }
}