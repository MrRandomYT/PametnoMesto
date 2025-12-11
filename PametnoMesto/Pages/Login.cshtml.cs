using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PametnoMesto.Scripts; // Pomembno za DataHandler
using System.Security.Claims;

namespace PametnoMesto.Pages
{
    public class LoginModel : PageModel
    {
        private readonly DataHandler _dataHandler;

        // Konstruktor
        public LoginModel(DataHandler dataHandler)
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

        public async Task<IActionResult> OnPostAsync()
        {
            // Uporabimo metodo ValidateUser iz DataHandlerja
            if (_dataHandler.ValidateUser(Username, Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToPage("/Index");
            }

            ErrorMessage = "Napačno uporabniško ime ali geslo!";
            return Page();
        }
    }
}