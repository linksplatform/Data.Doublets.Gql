using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Platform.Data.Doublets.Gql.Tests;

public static class TestExtensions
{
    public static Process RunServer(string tempFilePath)
    {
        var currentAssemblyDirectory = Directory.GetCurrentDirectory();
        var currentProjectDirectory = Path.GetFullPath(Path.Combine(currentAssemblyDirectory, "..", "..", ".."));
        var serverProjectDirectory = Path.GetFullPath(Path.Combine(currentProjectDirectory, "..", "Platform.Data.Doublets.Gql.Server"));
        var processStartInfo = new ProcessStartInfo { WorkingDirectory = serverProjectDirectory, FileName = "dotnet", Arguments = $"run -f net5 {tempFilePath}", RedirectStandardOutput = true, RedirectStandardInput = true};
        var process = Process.Start(processStartInfo);
        if (null == process || process.HasExited)
        {
            throw new Exception("Failed to start server process");
        }
        return process;
    }

    public static Uri GetEndPointFromServerProcess(Process process)
    {
        while (true)
        {
            var standartOutput = process?.StandardOutput;
            if(standartOutput == null)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                continue;
            }
            var processOutputLine = standartOutput.ReadLine();
            if (string.IsNullOrEmpty(processOutputLine))
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                continue;
            }
            if (processOutputLine.Contains("Unable to start"))
            {
                throw new Exception("Unable to start.");
            }
            if (processOutputLine.Contains("Now listening on: "))
            {
                var index = processOutputLine.IndexOf("Now listening on: ", StringComparison.Ordinal) + "Now listening on: ".Length;
                var uriString = processOutputLine.Substring(index);
                return new Uri($"{uriString}/v1/graphql");
            }
        }
    }
}
