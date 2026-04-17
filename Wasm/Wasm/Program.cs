var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();

app.UseBlazorFrameworkFiles(); //Endpoints for Blazor WebAssembly framework files.
app.UseStaticFiles(); //Endpoints for wwwroot files.
app.UseRouting();
app.MapControllers(); //for the API
app.MapFallbackToFile("index.html"); //Fallback for Blazor WebAssembly.
app.Run();