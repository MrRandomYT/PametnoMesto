using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class VehiclesModel : PageModel
    {
        private readonly DataHandler _dataHandler;
        
        [BindProperty]
        public int VehicleId { get; set; }

        // Filters
        [BindProperty(SupportsGet = true)] public string? SearchTerm { get; set; }

        [BindProperty(SupportsGet = true)] public string? TypeFilter { get; set; }

        [BindProperty(SupportsGet = true)] public bool? AvailabilityFilter { get; set; }
        
        [BindProperty(SupportsGet = true)] public int? HubFilter { get; set; }

        public List<Vehicle> Vehicles { get; private set; } = new();
        public List<string> VehicleTypes { get; set; } = new();
        
        public List<VehicleHub> VehicleHubs { get; set; } = new();
        public int SelectedHubId { get; set; }

        public VehiclesModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void OnGet()
        {
            VehicleHubs = _dataHandler.GetVehicleHubs()
                .Where(v => v.Capacity > 0)
                .ToList();

            List<Vehicle> allVehicles = _dataHandler.GetVehicles();
            //Vehicles = allVehicles;

            VehicleTypes = allVehicles
                .Select(v => v.Type.ToString())
                .Distinct()
                .OrderBy(t => t)
                .ToList();
            
            var hub = _dataHandler.GetVehicleHub(HubFilter);
            if (hub != null) allVehicles = hub.Vehicles;

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                allVehicles = allVehicles
                    .Where(v => v.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            
            if (!string.IsNullOrEmpty(TypeFilter))
            {
                allVehicles = allVehicles
                    .Where(v => v.Type.ToString() == TypeFilter)
                    .ToList();
            }
            
            if (AvailabilityFilter.HasValue)
            {
                allVehicles = allVehicles
                    .Where(v => v.IsAvailable == AvailabilityFilter.Value)
                    .ToList();
            }

            Vehicles = allVehicles;
        }
        public IActionResult OnPostRent(int id)
        {
            var username = User.Identity.Name;
            _dataHandler.RentVehicle(id, username);
            return RedirectToPage();
        }

        public IActionResult OnPostReturn(int id)
        {
            string message = _dataHandler.ReturnVehicle(id);
            TempData["Message"] = message; // Za izpis cene
            return RedirectToPage();
        }
    }
}