using App.Batches;
using Serilog;
using System.CommandLine;

var builder = WebApplication.CreateBuilder(args);

// Logging.
builder.Host.UseSerilog((context, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IBatch1Service, Batch1Service>();
builder.Services.AddSingleton<IBatch2Service, Batch2Service>();

var app = builder.Build();

// Root
var rootCommand = new RootCommand("Web");
rootCommand.SetHandler(_ =>
{
    RunWeb(app);
});

// Batch1
var batch1 = new Command("batch1", "Run batch1.");
batch1.SetHandler(_ =>
{
    RunBatch1(app);
});
rootCommand.AddCommand(batch1);

// Batch2
var batch2 = new Command("batch2", "Run batch2.");
batch2.SetHandler(_ =>
{
    RunBatch2(app);
});
rootCommand.AddCommand(batch2);

return await rootCommand.InvokeAsync(args);

void RunWeb(WebApplication app)
{
    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
    }
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}

void RunBatch1(WebApplication app)
{
    var batch1Service = app.Services.GetService<IBatch1Service>()!;
    batch1Service.DoBatch();
}

void RunBatch2(WebApplication app)
{
    var batch2Service = app.Services.GetService<IBatch2Service>()!;
    batch2Service.DoBatch();
}
