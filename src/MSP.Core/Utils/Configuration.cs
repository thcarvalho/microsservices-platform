using Microsoft.Extensions.Configuration;

namespace MSP.Core.Utils;

public static class Configuration
{
    public static IConfiguration GetConfiguration(string projectName, string environment = default)
    {
        if (string.IsNullOrWhiteSpace(environment))
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        return new ConfigurationBuilder()
            .SetBasePath(Project.GetDirectory(projectName))
            .AddJsonFile($"appsettings.{environment}.json")
            .Build();
    }
}