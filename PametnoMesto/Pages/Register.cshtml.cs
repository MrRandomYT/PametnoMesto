using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts;

namespace PametnoMesto.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        // Konstruktor: Tukaj dobimo dostop do tvojega DataHandlerja
        public RegisterModel(DataHandler dataHandler)
        {
            _dataHandler = dataHandler;
        }

        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            // Pokličemo novo metodo v DataHandlerju
            bool uspeh = _dataHandler.RegisterUser(Username, Password);

            if (uspeh)
            {
                // Če je registracija uspela, pojdi na prijavo
                return RedirectToPage("/Login");
            }
            else
            {
                // Če uporabnik že obstaja
                ErrorMessage = "Uporabnik s tem imenom že obstaja!";
                return Page();
            }
        }
    }
}