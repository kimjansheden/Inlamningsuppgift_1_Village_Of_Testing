using System.Diagnostics;
using System.Text.Json;
using GameLib;
using static GameLib.Strings.Message;

namespace Launcher;
class Program
{
    private static readonly string BlazorAppFolderPath = Path.GetFullPath(Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "BlazorApp", "bin", "Debug", "net6.0"));
    static void Main(string[] args)
    {
        Console.Clear();
        var strings = new Strings(new ConsoleUI());
        Console.WriteLine($"Choose UI. {Environment.NewLine}1. Console UI{Environment.NewLine}2. Web UI");
        var chosenUi = Console.ReadLine();
        switch (chosenUi)
        {
            case "1":
                Game game = new Game(new ConsoleUI());
                game.Run();
                break;
            case "2":
                LaunchWebApp();
                break;
            default:
                Console.WriteLine(strings.Messages[MenuEnterValidNumber]);
                break;
        }
    }

    private static void LaunchWebApp()
    {
        const string blazorAppDllPath = "BlazorApp.dll";
        const string dotnetPath = "dotnet";
        var contentRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", ".."));
        var url = GetApplicationUrlFromLaunchSettings();

        var startInfo = new ProcessStartInfo
        {
            FileName = dotnetPath,
            Arguments = $"\"{blazorAppDllPath}\" --content-root=\"{contentRootPath}\" --urls={url}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            WorkingDirectory = BlazorAppFolderPath,
            EnvironmentVariables =
            {
                ["ASPNETCORE_ENVIRONMENT"] = "Development"
            }
        };

        using var process = new Process
        {
            StartInfo = startInfo,
            EnableRaisingEvents = true
        };

        process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
        process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);
        process.Exited += (sender, e) => Console.WriteLine("Blazor app has exited.");

        process.Start();
        process.BeginOutputReadLine();
        process.BeginErrorReadLine();
        LaunchBrowser(url);
        
        // Wait for the Blazor app to exit before allowing the launcher program to exit.
        process.WaitForExit();
    }
    
    private static void LaunchBrowser(string url)
    {
        try
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true
            };
            Process.Start(processInfo);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to launch browser: {ex.Message}");
        }
    }
    private static string GetApplicationUrlFromLaunchSettings()
    {
        const string launchSettingsPath = "../../../../BlazorApp/Properties/launchSettings.json";
        var applicationUrl = "http://localhost:5058"; // Default URL in case the launchSettings.json file is not found or does not contain the applicationUrl setting.

        if (File.Exists(launchSettingsPath))
        {
            var jsonString = File.ReadAllText(launchSettingsPath);
            using JsonDocument doc = JsonDocument.Parse(jsonString);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("profiles", out JsonElement profiles) &&
                profiles.TryGetProperty("http", out JsonElement httpProfile) &&
                httpProfile.TryGetProperty("applicationUrl", out JsonElement applicationUrlElement))
            {
                applicationUrl = applicationUrlElement.GetString();
            }
        }
        return applicationUrl;
    }
}