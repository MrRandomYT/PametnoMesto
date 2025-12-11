using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PametnoMesto.Pages
{
    public class LogoutModel : PageModel
    {
        // Ta metoda se sproži, ko nekdo klikne gumb "Logout"
        public async Task<IActionResult> OnPostAsync()
        {
            // 1. Izbriši piškotek (uporabnik ni več prijavljen)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // 2. Preusmeri uporabnika nazaj na Login stran
            return RedirectToPage("/Login");
        }
    }
}