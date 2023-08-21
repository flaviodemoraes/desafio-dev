namespace DesafioDev.WebApi.Extensions
{
    public static class ConfigurationExtensions
    {
        public static IConfiguration GetConfiguration(string environmentName)
        {
            Console.WriteLine($"[ConfigurationExtensions] Environment: {environmentName}");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}
