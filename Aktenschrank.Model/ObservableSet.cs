using System.Collections.Specialized;
using System.ComponentModel;

namespace Aktenschrank.Model;

public class ObservableSet<T> : ICollection<T>, INotifyCollectionChanged, INotifyPropertyChanged
{
    private readonly HashSet<T> _set = new();

    public event NotifyCollectionChangedEventHandler? CollectionChanged;
    public event PropertyChangedEventHandler? PropertyChanged;

    public int Count => _set.Count;
    public bool IsReadOnly => false;

    public bool Add(T item)
    {
        if (_set.Add(item))
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));

            return true;
        }

        return false;
    }

    public void Clear()
    {
        if (_set.Count > 0)
        {
            _set.Clear();

            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));
        }
    }

    public bool Contains(T item) => _set.Contains(item);

    public void CopyTo(T[] array, int arrayIndex) => _set.CopyTo(array, arrayIndex);

    public bool Remove(T item)
    {
        int index = _set.ToList().IndexOf(item);

        if (_set.Remove(item))
        {
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, item, index));
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Count)));

            return true;
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator() => _set.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    void ICollection<T>.Add(T item) => Add(item);

    protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
        CollectionChanged?.Invoke(this, e);
    }

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        PropertyChanged?.Invoke(this, e);
    }
        
}