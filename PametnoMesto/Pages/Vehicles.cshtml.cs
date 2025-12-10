using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class VehiclesModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public List<Vehicle> Vehicles { get; private set; } = new();

        public VehiclesModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void OnGet()
        {
            Vehicles = _dataHandler.GetVehicles();
        }
    }
}