using System.Data;

namespace Library;

public class SudokuGrid
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

    public void SetValueAt(int row, int column)
    {
    }
}