using System;

namespace RimWorldSaveEditor.Models
{
    public interface IVersionInformation
    {
        Version Version { get; }
        Uri DownloadLocation { get; }
        //string ChangeLog { get; } TODO
    }
}