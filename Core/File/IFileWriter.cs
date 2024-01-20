namespace Redistributable_Wizard.Core.File;

public interface IFileWriter
{
    void Write(string path, byte[] bytes);
    Task WriteAsync(string path, byte[] bytes, CancellationToken cancellationToken);
}