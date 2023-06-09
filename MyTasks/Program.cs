using MudBlazor.Services;
using MyTasks.Data;
using MyTasks.Database;
using MyTasks.Interfaces;
using MyTasks.ReccuringTasksWorker;
using MyTasks.Services;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddHostedService<ReccuringTasksWorker>();
IConfiguration config = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();

builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddTransient<ITaskService, MyTaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

InitializeDatabase(app);

app.Run();

void InitializeDatabase(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    DatabaseContextInitializer.Initialize(context);
}
