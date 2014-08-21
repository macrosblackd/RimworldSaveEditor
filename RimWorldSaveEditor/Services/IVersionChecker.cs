using System;

namespace RimWorldSaveEditor.Services
{
    public interface IVersionChecker
    {
        Version Version { get; }
        string FormattedVersion { get; }
        bool UpdateAvailable { get; }
    }
}