using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Library;

public class SudokuGrid : INotifyPropertyChanged
{
    public HashSet<int> AllPossibilities { get; }
    private readonly int?[,] _cellValues;
    private readonly HashSet<int>[,] _cellPossibilities;
    public int Size { get; }

    public SudokuGrid(int size, HashSet<int> allPossibilities)
    {
        Size = size;
        AllPossibilities = allPossibilities;
        _cellValues = new int?[Size, Size];
        _cellPossibilities = new HashSet<int>[Size, Size];
        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
                _cellPossibilities[i, j] = [..allPossibilities]; // Interesting syntax o.o
        }
    }

    public int? GetValueAt(int row, int column)
    {
        return _cellValues[row, column];
    }

    public int? GetValueAt(Location location)
    {
        return GetValueAt(location.Row, location.Column);
    }

    public void SetValueAt(int row, int column, int value)
    {
        SetField(ref _cellValues[row, column], value);
    }

    public void SetValueAt(Location location, int value)
    {
        SetValueAt(location.Row, location.Column, value);
    }

    public HashSet<int> GetPossibilitiesAt(Location location)
    {
        return _cellPossibilities[location.Row, location.Column];
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
}