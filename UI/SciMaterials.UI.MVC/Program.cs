using SciMaterials.DAL.InitializationDb.Interfaces;
using SciMaterials.Services.API.Extensions;
using SciMaterials.UI.MVC.API.Middlewares;
using SciMaterials.UI.MVC.API.Extensions;
using SciMaterials.Domain.Extensions;
using SciMaterials.Services.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    var apiSettings = builder.Configuration.GetSection(ApiSettings.SectionName);
    var fileSize = apiSettings.GetValue<long>("MaxFileSize");
    serverOptions.Limits.MaxRequestBodySize = apiSettings.GetValue<long>("MaxFileSize");
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddSwagger(builder.Configuration);
builder.Services.ConfigureApplication(builder.Configuration);
builder.Services.AddMappings();

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    await dbInitializer.InitializeDbAsync(removeAtStart: true);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/MVC/Error");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.MapControllerRoute("default", "{controller}/{action=index}/{id?}");

app.Run();
