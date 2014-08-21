using System;

namespace RimWorldSaveEditor.Events
{
    public class VersionUpdateEventArgs : EventArgs
    {
        public VersionUpdateEventArgs(Version newVersion)
        {
            NewVersion = newVersion;
        }

        public Version NewVersion { get; private set; }
    }
}