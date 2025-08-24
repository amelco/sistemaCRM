using sistema.frontend.Components;
using sistema.frontend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<ClienteApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7204/"); // ajuste a porta da API
});

// Blazor Web App (novo modelo .NET 8+)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
