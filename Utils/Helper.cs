using Redistributable_Wizard.Core;
using System.Diagnostics;
using System.Security.Principal;

namespace Redistributable_Wizard.Utils;

public static class Helper
{
    internal enum Architecture
    {
        X64,
        X86
    }

    internal static readonly Dictionary<object, (string registryKey, string displayName, Architecture architecture, string downloadLink)> RedistributableKeys = new()
    {
        { PackageFinder.RedistributableVersionX64.VcRedist2005, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{ad8a2fa1-06e7-4b0d-927d-6e54b3d31028}", "Microsoft Visual C++ 2005 Redistributable (x64)", Architecture.X64, "https://download.microsoft.com/download/8/B/4/8B42259F-5D70-43F4-AC2E-4B208FD8D66A/vcredist_x64.EXE") }, //fine
        { PackageFinder.RedistributableVersionX64.VcRedist2008, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{5FCE6D76-F5DC-37AB-B2B8-22AB8CEDB1D4}", "Microsoft Visual C++ 2008 Redistributable - x64", Architecture.X64, "https://download.microsoft.com/download/5/D/8/5D8C65CB-C849-4025-8E95-C3966CAFD8AE/vcredist_x64.exe") }, //fine
        { PackageFinder.RedistributableVersionX64.VcRedist2010, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{1D8E6291-B0D5-35EC-8441-6616F567A0F7}", "Microsoft Visual C++ 2010  x64 Redistributable", Architecture.X64, "https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x64.exe") }, //fine - note: double white space in registry response ????
        { PackageFinder.RedistributableVersionX64.VcRedist2012, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}", "Microsoft Visual C++ 2012 Redistributable (x64)", Architecture.X86, "https://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU_4/vcredist_x64.exe") }, //changed, check again
        { PackageFinder.RedistributableVersionX64.VcRedist2013, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{042d26ef-3dbe-4c25-95d3-4c1b11b235a7}", "Microsoft Visual C++ 2013 Redistributable (x64)", Architecture.X86, "https://download.microsoft.com/download/2/E/6/2E61CFA4-993B-4DD4-91DA-3737CD5CD6E3/vcredist_x64.exe") }, //changed, check again
        { PackageFinder.RedistributableVersionX64.VcRedist2015_2017_2019_2022, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{1de5e707-82da-4db6-b810-5d140cc4cbb3}", "Microsoft Visual C++ 2015-2022 Redistributable (x64)", Architecture.X86, "https://download.visualstudio.microsoft.com/download/pr/a061be25-c14a-489a-8c7c-bb72adfb3cab/4DFE83C91124CD542F4222FE2C396CABEAC617BB6F59BDCBDF89FD6F0DF0A32F/VC_redist.x64.exe") }, //changed check again

        
        { PackageFinder.RedistributableVersionX86.VcRedist2005, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{710f4c1c-cc18-4c49-8cbf-51240c89a1a2}", "Microsoft Visual C++ 2005 Redistributable", Architecture.X86, "https://download.microsoft.com/download/8/B/4/8B42259F-5D70-43F4-AC2E-4B208FD8D66A/vcredist_x86.EXE") }, //fine
        { PackageFinder.RedistributableVersionX86.VcRedist2008, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{9BE518E6-ECC6-35A9-88E4-87755C07200F}", "Microsoft Visual C++ 2008 Redistributable - x86", Architecture.X86, "https://download.microsoft.com/download/5/D/8/5D8C65CB-C849-4025-8E95-C3966CAFD8AE/vcredist_x86.exe") }, //fine
        { PackageFinder.RedistributableVersionX86.VcRedist2010, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{F0C3E5D1-1ADE-321E-8167-68EF0DE699A5}", "Microsoft Visual C++ 2010  x86 Redistributable", Architecture.X86, "https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x86.exe") }, //fine - note: double white space in registry response wtf???
        { PackageFinder.RedistributableVersionX86.VcRedist2012, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}", "Microsoft Visual C++ 2012 Redistributable (x86)", Architecture.X86, "https://download.microsoft.com/download/1/6/B/16B06F60-3B20-4FF2-B699-5E9B7962F9AE/VSU_4/vcredist_x86.exe") }, //fine
        { PackageFinder.RedistributableVersionX86.VcRedist2013, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{9dff3540-fc85-4ed5-ac84-9e3c7fd8bece}", "Microsoft Visual C++ 2013 Redistributable (x86)", Architecture.X86, "https://download.microsoft.com/download/2/E/6/2E61CFA4-993B-4DD4-91DA-3737CD5CD6E3/vcredist_x86.exe") }, //fine
        { PackageFinder.RedistributableVersionX86.VcRedist2015_2017_2019_2022, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{2cfeba4a-21f8-4ea7-9927-c5a5c6f13cc9}", "Microsoft Visual C++ 2015-2022 Redistributable (x86)", Architecture.X86, "https://download.visualstudio.microsoft.com/download/pr/a061be25-c14a-489a-8c7c-bb72adfb3cab/C61CEF97487536E766130FA8714DD1B4143F6738BFB71806018EEE1B5FE6F057/VC_redist.x86.exe") }, //fine
    };

    internal static bool IsUserAdministrator()
    {
        var currentUser = WindowsIdentity.GetCurrent();
        var principal = new WindowsPrincipal(currentUser);
        return principal.IsInRole(WindowsBuiltInRole.Administrator);
    }

    internal static async Task<bool> RunOnDiskAsync(byte[] fileBytes, bool silentInstall)
    {
        try
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp", "vcinstaller.exe");
            await File.WriteAllBytesAsync(filePath, fileBytes);

            var startInfo = new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = silentInstall,
                Arguments = silentInstall ? "/Q" : null,
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
            File.Delete(filePath);

            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            throw new Exception("Error running the auto installer: " + ex.Message);
        }
    }

    internal static bool RunOnDisk(byte[] fileBytes, bool silentInstall)
    {
        try
        {
            var filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Temp", "vcinstaller.exe");
            File.WriteAllBytes(filePath, fileBytes);

            var startInfo = new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = silentInstall,
                Arguments = silentInstall ? "/Q" : null,
            };

            using var process = new Process { StartInfo = startInfo };
            process.Start();
            process.WaitForExit();
            File.Delete(filePath);

            return process.ExitCode == 0;
        }
        catch (Exception ex)
        {
            throw new Exception("Error running the auto installer: " + ex.Message);
        }
    }
}
