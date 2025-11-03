using System.IdentityModel.Tokens.Jwt;

using Homemade.Web.Components;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

var builder = WebApplication.CreateBuilder(args);
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHybridCache();
builder.AddRedisDistributedCache("cache");

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddKeycloakOpenIdConnect("keycloak", realm: "Homemade", OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.ClientId = "homemade.web";
        options.ClientSecret = builder.Configuration["Keycloak:ClientSecret"];
        options.ResponseType = OpenIdConnectResponseType.Code;
        options.Scope.Add("homemade:api");
        options.TokenValidationParameters.NameClaimType = JwtRegisteredClaimNames.Name;
        options.SaveTokens = true;
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

        // For development only - disable HTTPS metadata validation
        // In production, use explicit Authority configuration instead
        if (builder.Environment.IsDevelopment())
        {
            options.RequireHttpsMetadata = false;
        }
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

builder.Services.AddSearchClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/keycloak", () => Results.Challenge(
    properties: new Microsoft.AspNetCore.Authentication.AuthenticationProperties
    {
        RedirectUri = "/"
    },
    authenticationSchemes: [OpenIdConnectDefaults.AuthenticationScheme]
));
app.MapGet("/logout", () => Results.SignOut(
    properties: new Microsoft.AspNetCore.Authentication.AuthenticationProperties
    {
        RedirectUri = "/"
    },
    authenticationSchemes: [CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme]
));

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();