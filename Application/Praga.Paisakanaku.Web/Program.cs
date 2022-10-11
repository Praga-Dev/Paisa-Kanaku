using Praga.PaisaKanaku.Web.IoC;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    // add services
    using IHost host = Host.CreateDefaultBuilder(args).Build();
    IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

    string? connectionString = config["ConnectionStrings:DefaultConnection"];
    builder.Services.InjectDependencies(connectionString);

    // Serilog Configuration
    var logger = new LoggerConfiguration()
                        .ReadFrom.Configuration(builder.Configuration)
                        .Enrich.FromLogContext()
                        .CreateLogger();
    builder.Host.UseSerilog(logger);

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }
    
    app.UseSerilogRequestLogging();
    
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Error in Program.cs");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}