﻿using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Aktenschrank.Model;

public class Target : INotifyPropertyChanged
{
    private string _folderPath = string.Empty;

    private bool recursive;

    public Target(string folderPath)
    {
        FolderPath = folderPath;
    }

    public Target(string folderPath, bool recursive)
    {
        FolderPath = folderPath;
        Recursive = recursive;
    }


    public string FolderPath
    {
        get => _folderPath;
        set
        {
            if (value == _folderPath) return;
            _folderPath = value ?? throw new ArgumentNullException(nameof(value));
            OnPropertyChanged();
        }
    }

    public bool Recursive
    {
        get => recursive;
        set
        {
            if (value == recursive) return;
            recursive = value;
            OnPropertyChanged();
        }
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }

    public override string ToString()
    {
        return FolderPath;
    }

    protected bool Equals(Target other)
    {
        return _folderPath == other._folderPath;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Target)obj);
    }

    public override int GetHashCode()
    {
        return _folderPath.GetHashCode();
    }
}