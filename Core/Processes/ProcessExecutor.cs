using System.Diagnostics;
using Redistributable_Wizard.Core.Extensions;

namespace Redistributable_Wizard.Core.Processes;

public class ProcessExecutor : IProcessExecutor
{
    private const string SilentExecutionArgument = "/Q";

    public bool ExecuteProcess(string filePath, bool silentInstall)
    {
        var startInfo = GetStartInfo(filePath, silentInstall);

        return RunProcess(startInfo);
    }

    public async Task<bool> ExecuteProcessAsync(
        string filePath,
        bool silentInstall,
        CancellationToken cancellationToken)
    {
        var startInfo = GetStartInfo(filePath, silentInstall);

        return await RunProcessAsync(startInfo);
    }

    private static bool RunProcess(ProcessStartInfo startInfo)
    {
        using var process = new Process();
        process.StartInfo = startInfo;

        process.Start();
        process.WaitForExit();

        return process.IsSuccessful();
    }

    private static async Task<bool> RunProcessAsync(ProcessStartInfo startInfo)
    {
        using var process = new Process();
        process.StartInfo = startInfo;

        process.Start();
        await process.WaitForExitAsync();

        return process.IsSuccessful();
    }

    private static ProcessStartInfo GetStartInfo(string filePath, bool silentInstall)
    {
        return new ProcessStartInfo
        {
            FileName = filePath,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = silentInstall,
            Arguments = silentInstall ? SilentExecutionArgument : null,
        };
    }
}