using System.Diagnostics;

namespace Redistributable_Wizard.Core.Extensions;

public static class ProcessExtensions
{
    /// <summary>
    /// Check if a process exited successfully
    /// </summary>
    /// <param name="process">Process to check success state</param>
    /// <returns>True if the package process exited successfully</returns>
    public static bool IsSuccessful(this Process process)
    {
        return process.ExitCode == 0;
    }
}