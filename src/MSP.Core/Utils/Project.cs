namespace MSP.Core.Utils;

public static class Project
{
    public static string GetDirectory(string projectName)
    {
        var applicationBasePath = AppContext.BaseDirectory;
        var directoryInfo = new DirectoryInfo(applicationBasePath);

        do
        {
            directoryInfo = directoryInfo.Parent;
            var projectDirectoryInfo = new DirectoryInfo(directoryInfo.FullName);
            if (projectDirectoryInfo.Exists)
                if (new FileInfo(Path.Combine(projectDirectoryInfo.FullName, projectName, $"{projectName}.csproj")).Exists)
                    return Path.Combine(projectDirectoryInfo.FullName, projectName);
        }
        while (directoryInfo.Parent is not null);
        throw new Exception($"Project root could not be located using the application root {applicationBasePath}.");
    }
}