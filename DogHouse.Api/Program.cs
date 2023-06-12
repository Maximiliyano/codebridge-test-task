namespace DogHouse.Api;

public static class Program
{
    public static void Main(string[] args) =>
        CreateWebApplication(args).Build().Run();

    private static IHostBuilder CreateWebApplication(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());
}