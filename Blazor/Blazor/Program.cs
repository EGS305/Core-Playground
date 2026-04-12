using Blazor.Components;
using Blazor.Data;
using Blazor.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContextFactory<BlazorContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlazorContext") ?? throw new InvalidOperationException("Connection string 'BlazorContext' not found.")));

builder.Services.AddQuickGridEntityFrameworkAdapter();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//Add services to the container.
builder.Services.AddRazorComponents() //enable Razor
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddScoped<DataService>(); //The scope is the SignalR circuit.

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseAntiforgery(); //for forms
app.MapStaticAssets(); //Endpoints for images, css files etc.
app.MapRazorComponents<App>() //Endpoints for Razor components.
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Blazor.Client._Imports).Assembly);
app.Run();