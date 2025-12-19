using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class AddVehicleHubModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        public AddVehicleHubModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        [BindProperty]
        public string Name { get; set; } = "";

        //[BindProperty]
        //public string Address { get; set; } = "";

        [BindProperty]
        public int Capacity { get; set; } = 1;

        [BindProperty]
        public string Status { get; set; } = "Closed";
        
        [BindProperty] public double Latitude { get; set; } = 46.55906465244069;
        [BindProperty] public double Longitude { get; set; } = 15.638064980498713;

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            _dataHandler.AddVehicleHub(Name, Capacity,Longitude, Latitude, Status);
            return RedirectToPage("/VehicleHubs");
        }
    }
}