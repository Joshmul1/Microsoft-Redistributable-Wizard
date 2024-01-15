using Microsoft.Win32;
using static Redistributable_Wizard.Utils.Helper;

namespace Redistributable_Wizard.Core;

public static class PackageFinder
{
    public enum RedistributableVersionX64
    {
        VcRedist2005,
        VcRedist2008,
        VcRedist2010,
        VcRedist2012,
        VcRedist2013,
        VcRedist2015_2017_2019_2022
    }

    public enum RedistributableVersionX86
    {
        VcRedist2005,
        VcRedist2008,
        VcRedist2010,
        VcRedist2012,
        VcRedist2013,
        VcRedist2015_2017_2019_2022
    }
    
    /// <summary>
    /// Check if a specific Microsoft Redistributable Package is installed on the system
    /// </summary>
    /// <typeparam name="TVersion">The type of the version key used to identify the package.</typeparam>
    /// <param name="version">The version key of the package to be checked.</param>
    /// <returns>True if the package is installed; otherwise, false.</returns>
    /// <exception cref="Exception">Thrown when an error occurs during registry access.</exception>
    public static bool IsPackageInstalled<TVersion>(TVersion version)
    {
        if (version == null) return false;
        if (!RedistributableKeys.TryGetValue(version, out var registryInfo)) return false;
        
        try
        {
            var registryView = registryInfo.Arch == Architecture.X64 ? RegistryView.Registry64 : RegistryView.Registry32;
            using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
            using var subKey = baseKey.OpenSubKey(registryInfo.RegistryKey);
            var subKeyValue = subKey?.GetValue("DisplayName")?.ToString().ToLower();
            if (subKeyValue != null && subKeyValue.Contains(registryInfo.DisplayName.ToLower()))
                return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

        return false;
    }

    /// <summary>
    /// Checks for missing Visual C++ Redistributable packages in the system.
    /// </summary>
    /// <returns>
    /// A list of display names for the missing packages.
    /// If all packages are installed, returns a list with a single entry notifying that all packages are installed.
    /// </returns>
    public static List<string> GetAllMissingPackages()
    {
        var missingPackages = (from package in RedistributableKeys where !IsPackageInstalled(package.Key) select package.Value.DisplayName).ToList();

        if (missingPackages.Count == 0)
            missingPackages.Add("All packages are installed");

        return missingPackages;
    }

    /// <summary>
    /// Gets a list of all the current installed Visual C++ Redistributable packages on the system.
    /// </summary>
    /// <returns>
    /// A list of display names for the installed packages.
    /// </returns>
    public static List<string> GetAllInstalledPackages()
    {
        return (from package in RedistributableKeys where IsPackageInstalled(package.Key) select package.Value.DisplayName).ToList();
    }
}