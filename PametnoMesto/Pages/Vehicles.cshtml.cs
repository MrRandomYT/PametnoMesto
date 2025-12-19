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

        public List<Vehicle> Vehicles { get; private set; } = new();
        public List<string> VehicleTypes { get; set; } = new();

        public VehiclesModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public void OnGet()
        {
            List<Vehicle> allVehicles = _dataHandler.GetVehicles();
            //Vehicles = allVehicles;

            VehicleTypes = allVehicles
                .Select(v => v.Type.ToString())
                .Distinct()
                .OrderBy(t => t)
                .ToList();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                allVehicles = allVehicles
                    .Where(v => v.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }
            
            if (!string.IsNullOrWhiteSpace(TypeFilter))
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
        public IActionResult OnPostRemove()
        {
            if (VehicleId > 0)
            {
                _dataHandler.RemoveVehicle(VehicleId);
            }

            return RedirectToPage(); // refresh the page after removal
        }
        public IActionResult OnPostDeploy()
        {
            if (VehicleId > 0)
            {
                // Deployment Logic...
            }

            return RedirectToPage();
        }
    }
}