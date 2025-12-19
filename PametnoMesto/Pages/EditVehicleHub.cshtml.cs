using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;
using System.Collections.Generic;
using System.Linq;

namespace PametnoMesto.Pages
{
    public class EditVehicleHubModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public EditVehicleHubModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        public string Name { get; set; } = "";

        [BindProperty]
        public int Capacity { get; set; }

        [BindProperty]
        public string Status { get; set; } = "Closed";

        [BindProperty]
        public double Latitude { get; set; }

        [BindProperty]
        public double Longitude { get; set; }

        // Vehicles currently at this hub
        public List<Vehicle> Vehicles { get; set; } = new();

        // Available vehicles to add
        public List<Vehicle> AvailableVehicles { get; set; } = new();

        [BindProperty]
        public int SelectedVehicleId { get; set; } // vehicle chosen to add

        private int hubId;

        public IActionResult OnGet()
        {
            var hub = _dataHandler.GetVehicleHub(Id);
            if (hub == null) return RedirectToPage("/VehicleHubs");

            Name = hub.Name;
            Capacity = hub.Capacity;
            Status = hub.Status;
            Latitude = hub.Latitude;
            Longitude = hub.Longitude;

            Vehicles = hub.Vehicles;

            // Populate unassigned vehicles for dropdown
            AvailableVehicles = _dataHandler.GetVehicles()
                .Where(v => !_dataHandler.IsVehicleAssigned(v.Id))
                .ToList();

            return Page();
        }

        public IActionResult OnPostSave()
        {
            if (!ModelState.IsValid) return Page();

            _dataHandler.UpdateVehicleHub(Id, Name, Capacity, Longitude, Latitude, Status);
            return RedirectToPage(new { id = Id });
        }

        public IActionResult OnPostAddExistingVehicle()
        {
            var hub = _dataHandler.GetVehicleHub(Id);
            if (hub == null) return RedirectToPage("/VehicleHubs");

            var vehicle = _dataHandler.GetVehicle(SelectedVehicleId);
            if (vehicle == null) return RedirectToPage(new { id = Id });

            if (hub.Vehicles.Count >= hub.Capacity)
            {
                ModelState.AddModelError("", "Hub capacity reached. Cannot add more vehicles.");
                AvailableVehicles = _dataHandler.GetVehicles()
                    .Where(v => !_dataHandler.IsVehicleAssigned(v.Id))
                    .ToList();
                Vehicles = hub.Vehicles;
                return RedirectToPage(new { id = Id });
            }

            hub.Vehicles.Add(vehicle);

            // Refresh lists
            Vehicles = hub.Vehicles;
            AvailableVehicles = _dataHandler.GetVehicles()
                .Where(v => !_dataHandler.IsVehicleAssigned(v.Id))
                .ToList();

            return RedirectToPage(new { id = Id });
        }
        
        public IActionResult OnPostRemoveVehicle(int vehicleId)
        {
            var hub = _dataHandler.GetVehicleHub(Id);
            if (hub == null)
                return RedirectToPage("/VehicleHubs");

            var vehicle = hub.Vehicles.FirstOrDefault(v => v.Id == vehicleId);
            if (vehicle != null)
            {
                hub.Vehicles.Remove(vehicle);
            }

            return RedirectToPage(new { id = Id });
        }

    }
}
