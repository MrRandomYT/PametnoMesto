using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class RemoveVehicleModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public RemoveVehicleModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public List<Vehicle> Vehicles { get; private set; } = new();

        [BindProperty]
        public int VehicleId { get; set; }

        public void OnGet()
        {
            Vehicles = _dataHandler.GetVehicles();
        }

        public IActionResult OnPost()
        {
            _dataHandler.RemoveVehicle(VehicleId);
            return RedirectToPage("/Vehicles");
        }
    }
}