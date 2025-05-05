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
                _cellPossibilities[i, j] = [..allPossibilities];
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

    public void SetValueAt(int row, int column, int? value)
    {
        _cellValues[row, column] = value;
        UpdateAllPossibilities();
    }

    public void SetValueAt(Location location, int? value)
    {
        SetValueAt(location.Row, location.Column, value);
    }

    public void SetValueAtAndNotify(Location location, int? newValue)
    {
        SetValueAt(location, newValue);
        OnPropertyChanged();
    }

    public List<Location> GetAllLocations()
    {
        List<Location> locations = new List<Location>();
        for (int row = 0; row < Size; row++)
        {
            for (int col = 0; col < Size; col++)
            {
                locations.Add(new Location(row, col));
            }
        }
        return locations;
    }

    public void ClearBoard()
    {
        var locations = GetAllLocations();
        foreach (var location in locations)
        {
            SetValueAt(location, null);
        }
        UpdateAllPossibilities();
        OnPropertyChanged();
    }
    
    public void UpdateAllPossibilities()
    {
        foreach (var location in GetAllLocations())
        {
            if (_cellValues[location.Row, location.Column] is not null)
            {
                _cellPossibilities[location.Row,location.Column].Clear();
            }
            else
            {
                UpdatePossibilitiesAt(location);
            }
        }
    }

    private void UpdatePossibilitiesAt(Location location)
    {
        var associatedLocations = location.GetAssociatedLocations(Size);
        var newPossibilities = new HashSet<int>(AllPossibilities);
        foreach (var associatedLocation in associatedLocations)
        {
            int? valueAtLocation = GetValueAt(associatedLocation); 
            if (valueAtLocation is not null)
            {
                newPossibilities.Remove(valueAtLocation.Value);
            }
        }
        _cellPossibilities[location.Row, location.Column] = newPossibilities;
    }

    public HashSet<int> GetPossibilitiesAt(Location location)
    {
        return _cellPossibilities[location.Row, location.Column];
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

    public bool FillInCellsWithOneOption()
    {
        bool somethignChanged = false;
        foreach (var location in GetAllLocations())
        {
            if (GetValueAt(location) is not null) continue;
            var possibilities = GetPossibilitiesAt(location);
            if (possibilities.Count == 1)
            {
                SetValueAt(location, possibilities.First());
                somethignChanged = true;
            }
        }
        UpdateAllPossibilities();
        return somethignChanged;
    }

    public void SimpleSolve()
    {
        bool somethingChanged;
        do
        {
            somethingChanged = FillInCellsWithOneOption();
        } while (somethingChanged);
    }
    
    
}