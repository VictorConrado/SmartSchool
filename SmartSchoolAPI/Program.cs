using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Hosting;
using SmartSchoolAPI;

var builder = WebApplication.CreateBuilder(args);

// Use Startup class for configuration
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

startup.Configure(app, app.Environment, provider);

app.Run();
