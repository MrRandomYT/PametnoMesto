using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;
using System.Collections.Generic;
using System.Linq;

namespace PametnoMesto.Pages
{
    public class HubMapModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public HubMapModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public List<VehicleHub> VehicleHubs { get; set; } = new();

        public string? SearchTerm { get; set; }
        public string? StatusFilter { get; set; }

        public void OnGet(string? searchTerm, string? statusFilter)
        {
            SearchTerm = searchTerm;
            StatusFilter = statusFilter;

            var hubs = _dataHandler.GetVehicleHubs();

            if (!string.IsNullOrWhiteSpace(SearchTerm))
            {
                hubs = hubs
                    .Where(h => h.Name.Contains(SearchTerm, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(StatusFilter))
            {
                hubs = hubs
                    .Where(h => h.Status == StatusFilter)
                    .ToList();
            }

            VehicleHubs = hubs;
        }
    }
}