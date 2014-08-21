using System;
using System.Net;
using System.Reflection;

namespace RimWorldSaveEditor.Services
{
    public class VersionChecker : IVersionChecker
    {
        private const string ReleaseThreadUrl = @"http://ludeon.com/forums/index.php?topic=5346.0";
        private const string VersionCheckUrl = @"http://pastebin.com/raw.php?i=3LvpsTWB";
        private readonly Version currentVersion;

        public VersionChecker(Assembly assemblyToCheck)
        {
            currentVersion = assemblyToCheck.GetName().Version;
        }

        public Version Version
        {
            get { return this.currentVersion; }
        }

        public string FormattedVersion {
            get { return this.currentVersion.ToString(4); }
        }

        public bool UpdateAvailable
        {
            get { return this.CheckForUpdate(); }
        }

        private bool CheckForUpdate()
        {
            // Dependency on Configuration setting
            var shouldCheck = new Random().Next(0, 1) == 1;
            if (!shouldCheck) return false;

            string latestFormattedVersion;
            //Dependency on WebClient
            using (var client = new WebClient())
            {
                latestFormattedVersion = client.DownloadString(VersionCheckUrl);
            }

            var latestVersion = new Version(latestFormattedVersion);
            return latestVersion > currentVersion;
        }
    }
}