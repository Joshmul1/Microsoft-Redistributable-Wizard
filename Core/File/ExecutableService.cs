using Redistributable_Wizard.Core.Processes;

namespace Redistributable_Wizard.Core.File;

public class ExecutableService : IExecutableService
{
  
    private readonly IProcessExecutor _processExecutor;
    private readonly IFileWriter _fileWriter;
    
    private const string ExecutableName = "vcinstaller.exe";
    private static string FilePath => Path.Combine(
                $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Temp", ExecutableName);

    public ExecutableService(IProcessExecutor processExecutor, IFileWriter fileWriter)
    {
        _processExecutor = processExecutor;
        _fileWriter = fileWriter;
    }
    
    public bool Execute(byte[] bytes, bool silentInstall)
    {
        _fileWriter.Write(FilePath, bytes);
        return _processExecutor.ExecuteProcess(FilePath, silentInstall);
    }

    public async Task<bool> ExecuteAsync(
        byte[] bytes,
        bool silentInstall,
        CancellationToken cancellationToken)
    {
        await _fileWriter.WriteAsync(FilePath, bytes, cancellationToken);
        return await _processExecutor.ExecuteProcessAsync(FilePath, silentInstall, cancellationToken);
    }
}