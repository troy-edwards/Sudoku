using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library;

public class GameState : INotifyPropertyChanged
{
    public SudokuGrid CurrentBoard { get; }
    private Location? _selectedLocation;
    public Location? SelectedLocation
    {
        get => _selectedLocation;
        set
        {
            SetField(ref _selectedLocation, value);
            OnPropertyChanged();
            
        }
    }

    public GameState(int size = 9, HashSet<int>? possibilities = null)
    {
        possibilities ??= [1, 2, 3, 4, 5, 6, 7, 8, 9];
        CurrentBoard = new SudokuGrid(size, possibilities);
    }


    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
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
}