# How To Add Razor Pages To An Existing ASP.NET Core Web Project

I almost always start with an empty ASP.NET Core project (e.g., `dotnet new web`) and add what I want and need. This is how I add Razor pages when I want it.

## Add Services

In `Program.cs` (or whever else these have moved to):

```
builder.Services.AddRazorPages();
```

## Configure App

In `Program.cs` (or whever else these have moved to):

```
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapRazorPages();
```

## Add Static `wwwroot` Files

I don't know of an easier way to get the static files than to create an empty Razor app and copy over the `wwwroot` directory.

```
dotnet new webapp -o ./Temporary
cp -r ./Temporary/wwwroot <project-root>/
rm -rf ./Temporary
```
