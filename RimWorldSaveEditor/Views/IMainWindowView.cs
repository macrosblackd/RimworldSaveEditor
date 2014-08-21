using System;
using RimWorldSaveEditor.Events;

namespace RimWorldSaveEditor.Views
{
    public interface IMainWindowView
    {
        event EventHandler OnSave;
        event EventHandler OnOpenFile;

        void SaveHandled();
        void OpenFileHandled();
        void HandleNewVersion(VersionUpdateEventArgs versionUpdated);
    }
}