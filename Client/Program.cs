using Client;
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
builder.Services.AddSingleton<ILogService, LogService>();

#endregion \CustomServices



await builder.Build().RunAsync();
