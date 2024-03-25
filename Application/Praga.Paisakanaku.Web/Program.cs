using Praga.Paisakanaku.Web.Middleware;
using Praga.PaisaKanaku.Web.IoC;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    builder.WebHost.UseUrls("http://192.168.29.1:8080");
    builder.WebHost.ConfigureKestrel((context, serverOptions) =>
    {
        serverOptions.ListenAnyIP(8080);
        
        var kestrelSection = context.Configuration.GetSection("Kestrel");
        serverOptions.Configure(kestrelSection)
            .Endpoint("HTTPS", listenOptions =>
            {
                // ...
            });
    });

    //Add support to logging with SERILOG
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    // add services
    using IHost host = Host.CreateDefaultBuilder(args).Build();
    IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

    string connectionString = config["ConnectionStrings:DefaultConnection"]
        ?? throw new ArgumentNullException("ConnectionStrings:DefaultConnection is null");
    builder.Services.InjectDependencies(connectionString);

    builder.Services.AddControllersWithViews();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseMiddleware<AuthenticationMiddleware>();
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