# How To Add Auth0 To an Existing ASP.NET Web Project

_This assumes starting with an empty ASP.NET web project (i.e. `dotnet new web`) made with .NET 6, but should be easily adapted to an existing web project._

## Create the Auth0 Application

In your Auth0 tenant, create a new application of type `Regular Web Application`. Note the following settings:

- Domain
- Client ID
- Client secret

## Add Allowed URLs to Auth0 Applications

In your Auth0 tenant, in the application you created above, add the following setting values:

- Allowed Callback URLs: `https://localhost:<port>/callback`
- Allowed Logout URLs: `https://localhost:<port>`

## Install Auth0 NuGet Package

```
dotnet add package Auth0.AspNetCore.Authentication
```

## Register Authentication in Services

In `Program.cs` (or wherever else these have moved to), add:

```
builder.Services.AddAuthentication().AddCookie(options => {
    options.LoginPath = "/SignIn"; // <-- this is to override the default of /Account/Login
    options.LoginPath = "/SignOut"; // <-- this is to override the default of /Account/Logout
});

builder.Services.AddAuth0WebAppAuthentication(options => {
    options.Domain = builder.Configuration["AUTH0_DOMAIN"] ;
    options.ClientId = builder.Configuration["AUTH0_CLIENT_ID"];
    options.ClientSecret = builder.Configuration["AUTH0_CLIENT_SECRET"];
    options.Scope = "openid profile email";
    options.SkipCookieMiddleware = true; // <-- this is needed because we added cookie auth directly above to change the login path
}).WithAccessToken(options => {
    options.Audience = builder.Configuration["AUTH0_AUDIENCE"];
});
```

## Add Authentication To App

In `Program.cs` (or wherever else these have moved to), between `app.UseRouting` and `app.MapRazorPages`, add:

```
app.UseAuthentication();
app.UseAuthorization();
```

## Add Sign In and Sign Out Pages

Create a SignIn.cshtml and SignOut.cshtml.cs file in `/Pages`.

In SignIn.cshtml.cs, add the following:

```
public async Task OnGet()
{
    var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
        .Build();

    await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
}
```

In SignOut.cshtml.cs, add the following:

```
public async Task OnGet()
{
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme);

    return Redirect($"https://{Configuration["AUTH0_DOMAIN"]}/v2/logout?client_id={Configuration["AUTH0_CLIENT_ID"]}&returnTo=https://{Request.Host}/");
}
```

## Set Auth0 Configuration Values

There are four .NET configuration values to set:

- `AUTH0_DOMAIN`: This comes from the application settings in Auth0
- `AUTH0_CLIENT_ID`: This comes from the application settings in Auth0
- `AUTH0_CLIENT_SECRET`: This comes from the application settings in Auth0
- `AUTH0_AUDIENCE`: This is needed if you want to use an access token for API auth, and if so, should be the API identifier in Auth0

Some of these are secret and should not be put directly in config. You can use .NET's user secrets for these locally and another mechanism in remote environments, or you can just use environment variables.