using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DungeonCrawlBenchmark.Web
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IndexModel()
        {
            AccessToken = "None";
        }
        
        public async Task OnGet()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            if (accessToken is not null) {
                AccessToken = accessToken;
            }
        }

        public string AccessToken { get; set; }
    }
}
