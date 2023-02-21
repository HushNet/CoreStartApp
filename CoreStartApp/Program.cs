using CoreStartApp.Middlewares;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// if (app.Environment.IsDevelopment())
// {
//     app.UseDeveloperExceptionPage();
// }

app.UseMiddleware<LoggingMiddleware>();

app.MapGet("/", async context =>
{
    await context.Response.WriteAsync($"Welcome to the {app.Environment.ApplicationName}!");
});


app.Map("/about", About);
app.Map("/config", Config);

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "Views"))
});

app.UseStatusCodePages();


app.Run();



void About(IApplicationBuilder builder)
{
    builder.Run(async context =>
    {
        var a = 1;
        var b = 0;
        var c = a / b;
        await context.Response.WriteAsync($"{app.Environment.ApplicationName} - ASP.Net Core tutorial project");
    });
}

void Config(IApplicationBuilder builder)
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync($"App name: {app.Environment.ApplicationName}. App running configuration: {app.Environment.EnvironmentName}");
    });
}