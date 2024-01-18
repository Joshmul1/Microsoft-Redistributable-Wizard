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
        if (version is null) return false;
        if (!RedistributableKeys.TryGetValue(version, out var dictionary)) return false;
        
        try
        {
            var registryView = dictionary.architecture == Architecture.X64 ? RegistryView.Registry64 : RegistryView.Registry32;
            using var baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, registryView);
            using var subKey = baseKey.OpenSubKey(dictionary.registryKey);
            var subKeyValue = subKey?.GetValue("DisplayName")?.ToString().ToLower();
            if (subKeyValue != null && subKeyValue.Contains(dictionary.displayName.ToLower()))
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
        var missingPackages = (from package in RedistributableKeys where !IsPackageInstalled(package.Key) select package.Value.displayName).ToList();

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
        return (from package in RedistributableKeys where IsPackageInstalled(package.Key) select package.Value.displayName).ToList();
    }

    /// <summary>
    /// Installs a specified package version, optionally running the installer silently.
    /// </summary>
    /// <typeparam name="TVersion">The version of the package to install.</typeparam>
    /// <param name="version">The version of the package to install.</param>
    /// <param name="silentInstall">Specifies whether to run the installer silently (no visible window).</param>
    /// <exception cref="Exception">Thrown if the download or installation encounters an error, or the user is not an Administrator</exception>
    public static async Task InstallPackageAsync<TVersion>(TVersion version, bool silentInstall = false)
    {
        if (version is null) return;
        if (!RedistributableKeys.TryGetValue(version, out var dictionary)) return;

        if (!IsUserAdministrator())
            throw new Exception("User is not running as an Administrator");
        
        try
        {
            var downloadLink = dictionary.downloadLink;
            if (!string.IsNullOrEmpty(downloadLink))
            {
                using var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(downloadLink);

                if (response.IsSuccessStatusCode)
                {
                    var fileBytes = await response.Content.ReadAsByteArrayAsync();
                    if (await RunOnDiskAsync(fileBytes, silentInstall)) return;
                }

                throw new Exception($"Failed to download the file from {downloadLink}. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    /// <summary>
    /// Installs a specified package version, optionally running the installer silently.
    /// </summary>
    /// <typeparam name="TVersion">The version of the package to install.</typeparam>
    /// <param name="version">The version of the package to install.</param>
    /// <param name="silentInstall">Specifies whether to run the installer silently (no visible window).</param>
    /// <exception cref="Exception">Thrown if the download or installation encounters an error, or the user is not an Administrator</exception>
    public static void InstallPackage<TVersion>(TVersion version, bool silentInstall = false)
    {
        if (version is null) return;
        if (!RedistributableKeys.TryGetValue(version, out var dictionary)) return;

        if (!IsUserAdministrator())
            throw new Exception("User is not running as an Administrator");

        try
        {
            var downloadLink = dictionary.downloadLink;
            if (!string.IsNullOrEmpty(downloadLink))
            {
                using var httpClient = new HttpClient();
                var response = httpClient.GetAsync(downloadLink).Result;

                if (response.IsSuccessStatusCode)
                {
                    var fileBytes = response.Content.ReadAsByteArrayAsync().Result;
                    if (RunOnDisk(fileBytes, silentInstall)) return;
                }

                throw new Exception($"Failed to download the file from {downloadLink}. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}