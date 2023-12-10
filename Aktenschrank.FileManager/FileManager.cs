using System.Collections.ObjectModel;
using Aktenschrank.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Aktenschrank.Sorting;

public class FileManager : ObservableObject
{
    private ObservableCollection<FileManagingProfile> _scheduledProfiles = new();

    private ObservableCollection<FileManagingTask> _fileManagingTasks = new();

    public ObservableCollection<FileManagingTask> FileManagingTasks
    {
        get => _fileManagingTasks;
        set
        {
            if (Equals(value, _fileManagingTasks)) return;
            _fileManagingTasks = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }
}