using Redistributable_Wizard.Core;

namespace Redistributable_Wizard.Utils;

public static class Helper
{
    internal enum Architecture
    {
        X64,
        X86
    }

    internal static readonly Dictionary<object, (string RegistryKey, string DisplayName, Architecture Arch)> RedistributableKeys = new()
    {
            { PackageFinder.RedistributableVersionX64.VcRedist2005, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{ad8a2fa1-06e7-4b0d-927d-6e54b3d31028}", "Microsoft Visual C++ 2005 Redistributable (x64)", Architecture.X64) }, //fine
            { PackageFinder.RedistributableVersionX64.VcRedist2008, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{5FCE6D76-F5DC-37AB-B2B8-22AB8CEDB1D4}", "Microsoft Visual C++ 2008 Redistributable - x64", Architecture.X64) }, //fine
            { PackageFinder.RedistributableVersionX64.VcRedist2010, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{1D8E6291-B0D5-35EC-8441-6616F567A0F7}", "Microsoft Visual C++ 2010  x64 Redistributable", Architecture.X64) }, //fine - note: double white space in registry response ????
            { PackageFinder.RedistributableVersionX64.VcRedist2012, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{ca67548a-5ebe-413a-b50c-4b9ceb6d66c6}", "Microsoft Visual C++ 2012 Redistributable (x64)", Architecture.X86) }, //changed, check again
            { PackageFinder.RedistributableVersionX64.VcRedist2013, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{042d26ef-3dbe-4c25-95d3-4c1b11b235a7}", "Microsoft Visual C++ 2013 Redistributable (x64)", Architecture.X86) }, //changed, check again
            { PackageFinder.RedistributableVersionX64.VcRedist2015_2017_2019_2022, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{1de5e707-82da-4db6-b810-5d140cc4cbb3}", "Microsoft Visual C++ 2015-2022 Redistributable (x64)", Architecture.X86) }, //changed check again

            
            { PackageFinder.RedistributableVersionX86.VcRedist2005, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{710f4c1c-cc18-4c49-8cbf-51240c89a1a2}", "Microsoft Visual C++ 2005 Redistributable", Architecture.X86) }, //fine
            { PackageFinder.RedistributableVersionX86.VcRedist2008, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{9BE518E6-ECC6-35A9-88E4-87755C07200F}", "Microsoft Visual C++ 2008 Redistributable - x86", Architecture.X86) }, //fine
            { PackageFinder.RedistributableVersionX86.VcRedist2010, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{F0C3E5D1-1ADE-321E-8167-68EF0DE699A5}", "Microsoft Visual C++ 2010  x86 Redistributable", Architecture.X86) }, //fine - note: double white space in registry response wtf???
            { PackageFinder.RedistributableVersionX86.VcRedist2012, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{33d1fd90-4274-48a1-9bc1-97e33d9c2d6f}", "Microsoft Visual C++ 2012 Redistributable (x86)", Architecture.X86) }, //fine
            { PackageFinder.RedistributableVersionX86.VcRedist2013, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{9dff3540-fc85-4ed5-ac84-9e3c7fd8bece}", "Microsoft Visual C++ 2013 Redistributable (x86)", Architecture.X86) }, //fine
            { PackageFinder.RedistributableVersionX86.VcRedist2015_2017_2019_2022, (@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\{2cfeba4a-21f8-4ea7-9927-c5a5c6f13cc9}", "Microsoft Visual C++ 2015-2022 Redistributable (x86)", Architecture.X86) }, //fine
        };
}
