using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DungeonCrawlBenchmark.Web
{
    public class SignOutModel : PageModel
    {
        public SignOutModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public async Task<IActionResult> OnGet()
        {
            // NB! I am not sure these are both needed, but some Auth0 docs showed it, and it sort of makes sense given
            //     the Auth0 flow uses cookie auth.
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme);

            return Redirect($"https://{Configuration[Constants.ConfigurationKeys.Auth0Domain]}/v2/logout?client_id={Configuration[Constants.ConfigurationKeys.Auth0ClientId]}&returnTo=https://{Request.Host}/");
        }
    }
}
