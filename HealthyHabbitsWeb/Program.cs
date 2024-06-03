using HealthyHabbitsWeb.Components;
using HealthyHabbitsWeb.Client;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
.AddInteractiveServerComponents();


builder.Services.AddAuthentication(o =>
{
    o.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(opts =>
{
    opts.Cookie.Name = "auth_token";
    opts.LoginPath = "/login";
    opts.Cookie.MaxAge = TimeSpan.FromMinutes(30);
    opts.AccessDeniedPath = "/access-denied";
});
//builder.Services.AddScoped<HttpContext>();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthorizationCore();

// Register the AuthenticationStateProvider
builder.Services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();



builder.Services.AddHttpContextAccessor();

var apiUrl = builder.Configuration["ApiUrl"] ?? 
    throw new Exception("ApiUrl not set");

builder.Services.AddScoped<HabbitClient>();
builder.Services.AddScoped<DbService>();

//builder.Services.AddHttpClient(apiUrl);

builder.Services.AddHttpClient<HabbitClient>(c => {
    c.BaseAddress = new Uri(apiUrl);
});

builder.Services.AddHttpClient<DbService>(c => {
    c.BaseAddress = new Uri(apiUrl);
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseStaticFiles();
app.UseAntiforgery();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
