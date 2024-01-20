namespace Redistributable_Wizard.Core.Processes;

public interface IProcessExecutor
{
    bool ExecuteProcess(string filePath, bool silentInstall);
    Task<bool> ExecuteProcessAsync(string filePath, bool silentInstall, CancellationToken cancellationToken);
}