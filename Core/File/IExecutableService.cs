namespace Redistributable_Wizard.Core.File;

public interface IExecutableService
{
    bool Execute(byte[] bytes, bool silentInstall);

    Task<bool> ExecuteAsync(
        byte[] bytes,
        bool silentInstall,
        CancellationToken cancellationToken);
}