using System.Reflection;
using DogHouse.BLL.Interfaces.Repositories;
using DogHouse.BLL.Interfaces.Services;
using DogHouse.BLL.Repositories;
using DogHouse.BLL.Services;
using DogHouse.Common.MappingProfiles;

namespace DogHouse.Api.Extensions;

public static class ServiceExtension
{
    public static void RegisterCustomServices(this IServiceCollection service)
    {
        service.AddScoped<IDogService, DogService>();
    }

    public static void RegisterRepositories(this IServiceCollection service)
    {
        service.AddScoped<IDogRepository, DogRepository>();
    }

    public static void RegisterAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
          cfg.AddProfile<DogProfile>();
        },
        Assembly.GetExecutingAssembly());
    }
}