using Library;

namespace Sudoku.Tests;

[TestFixture]
public class SudokuGridTests
{
    [Test]
    public void AllCellsStartAsNull()
    {
        int size = 9;
        var grid = new SudokuGrid(size, new HashSet<int> {1, 2, 3});

        int nullCount = 0;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (grid.GetValueAt(row, col) is null)
                    nullCount++;
            }
        }

        Assert.That(nullCount, Is.EqualTo(size * size));
    }
}