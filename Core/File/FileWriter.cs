namespace Redistributable_Wizard.Core.File;

public class FileWriter : IFileWriter
{
    public void Write(string path, byte[] bytes)
    {
        System.IO.File.WriteAllBytes(path, bytes);
    }

    public async Task WriteAsync(string path, byte[] bytes, CancellationToken cancellationToken)
    {
        await System.IO.File.WriteAllBytesAsync(path, bytes, cancellationToken);
    }
}