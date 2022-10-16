using Serilog;
using Praga.PaisaKanaku.API.IoC;

try
{
    var builder = WebApplication.CreateBuilder(args);

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

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    // Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Praga.Paisakanaku.API"));

    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
    app.UseSerilogRequestLogging();

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
