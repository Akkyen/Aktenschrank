﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace Aktenschrank.Model;

public class Target : ObservableObject, ICloneable
{
    private Guid _id = Guid.NewGuid();

    private string _folderPath = string.Empty;

    private bool _enabled;
    private bool _recursive;

    public Target(string folderPath)
    {
        FolderPath = folderPath;
    }

    public Target(string folderPath, bool recursive)
    {
        FolderPath = folderPath;
        Recursive = recursive;
    }

    public Target(string folderPath, bool enabled, bool recursive)
    {
        FolderPath = folderPath;
        Enabled = enabled;
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

    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (value == _enabled) return;
            _enabled = value;
            OnPropertyChanged();
        }
    }

    public bool Recursive
    {
        get => _recursive;
        set
        {
            if (value == _recursive) return;
            _recursive = value;
            OnPropertyChanged();
        }
    }

    public override string ToString()
    {
        return FolderPath;
    }

    public object Clone()
    {
        return new Target(_folderPath, _enabled, _recursive);
    }

    protected bool Equals(Target other)
    {
        return _id.Equals(other._id);
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
        return _id.GetHashCode();
    }
}