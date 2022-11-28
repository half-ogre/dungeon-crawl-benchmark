using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DungeonCrawlBenchmark.Web
{
    public class SignInModel : PageModel
    {
        public async Task OnGet()
        {
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }
    }
}
