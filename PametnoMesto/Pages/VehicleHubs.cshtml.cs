using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class VehicleHubsModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public VehicleHubsModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; } = "";

        public List<VehicleHub> Hubs { get; set; } = new();

        [BindProperty]
        public int HubId { get; set; }

        public void OnGet()
        {
            Hubs = string.IsNullOrWhiteSpace(SearchTerm)
                ? _dataHandler.GetVehicleHubs()
                : _dataHandler.GetVehicleHubs()
                    .Where(h => h.Name.Contains(SearchTerm, StringComparison.OrdinalIgnoreCase))
                    .ToList();
        }

        public IActionResult OnPostRemove()
        {
            if (HubId > 0)
            {
                _dataHandler.RemoveVehicleHub(HubId);
            }

            return RedirectToPage();
        }
    }
}