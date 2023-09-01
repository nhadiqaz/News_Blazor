using Blazored.LocalStorage;
using Client;
using Client.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

#region HttpClient

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri("https://localhost:7151/") 
});

#endregion \HttpClient

#region CustomServices

builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ILogService, LogService>();
builder.Services.AddScoped<IAutenticationService, AutenticationService>();

#endregion \CustomServices

#region Authentication

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
//builder.Services.AddApiAuthorization();

#endregion \Authentication



await builder.Build().RunAsync();
