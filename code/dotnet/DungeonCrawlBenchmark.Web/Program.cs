using Auth0.AspNetCore.Authentication;
using DungeonCrawlBenchmark.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddAuthentication().AddCookie(options => {
    options.LoginPath = "/SignIn";
    options.LogoutPath = "/SignOut";
});

builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration[Constants.ConfigurationKeys.Auth0Domain];
    options.ClientId = builder.Configuration[Constants.ConfigurationKeys.Auth0ClientId];
    options.ClientSecret = builder.Configuration[Constants.ConfigurationKeys.Auth0ClientSecret];
    options.Scope = "openid profile email";
    options.SkipCookieMiddleware = true;
}).WithAccessToken(options => {
    options.Audience = builder.Configuration[Constants.ConfigurationKeys.Auth0Audience];
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
