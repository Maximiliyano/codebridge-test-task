using DogHouse.Api.Extensions;
using DogHouse.DAL.Context;
using Microsoft.EntityFrameworkCore;

namespace DogHouse.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(
        IServiceCollection services)
    {
        var migrationAssembly = typeof(DogHouseDbContext).Assembly.GetName().Name;
        services.AddDbContext<DogHouseDbContext>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("DbConnection"),
                opt => opt.MigrationsAssembly(migrationAssembly)));

        services.RegisterCustomServices();
        services.RegisterAutoMapper();
        services.RegisterRepositories();
        
        services.AddControllers();
        services.AddSwaggerGen();
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseEndpoints(cfg =>
        {
            cfg.MapControllers();
        });

        InitializeDatabase(app);
    }

    private static void InitializeDatabase(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices
            .GetService<IServiceScopeFactory>()?
            .CreateScope();

        using var context = scope?.ServiceProvider
            .GetRequiredService<DogHouseDbContext>();

        context?.Database.Migrate();
    }
}