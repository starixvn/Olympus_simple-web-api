
using SimpleWebApi;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container.

services.AddHealthChecks();

// Register memory cache
services.AddMemoryCache();
services.AddControllers();

var app = builder.Build();


app.MapControllers();
app.MapHealthChecks("/healthz");

await app.RunAsync();
