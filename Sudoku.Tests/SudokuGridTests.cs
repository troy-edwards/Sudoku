using Library;

namespace Sudoku.Tests;

[TestFixture]
public class SudokuGridTests
{
    private static readonly HashSet<int> FullPossibilities =
    [
        1, 2, 3, 4, 5, 6, 7, 8, 9
    ];
    
    [Test]
    public void AllCellsStartAsNull()
    {
        int size = 9;
        var grid = new SudokuGrid(size, [1, 2, 3]);

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

    [Test]
    public void GridStartsWith9OptionsInAllCellsForSize9()
    {
        SudokuGrid grid = new SudokuGrid(9, FullPossibilities);

        var someLocationPossibilities = grid.GetPossibilitiesAt(new Location(3, 7));

        Assert.That(someLocationPossibilities, Is.EquivalentTo(FullPossibilities));
    }

    [Test]
    public void AddingAnOptionToGridRemovesThatNumberAsPossibilityFromAllAssociatedCells()
    {
        int size = 9;
        SudokuGrid grid = new SudokuGrid(size, FullPossibilities);
        Location location = new Location(3, 6);
        int valueToSetAtLocation = 1;
        
        var associatedLocations = location.GetAssociatedLocations(size);
        grid.SetValueAt(location, valueToSetAtLocation);

        foreach (Location associatedLocation in associatedLocations)
        {
            bool hasRemovedNumber = grid.GetPossibilitiesAt(associatedLocation).Contains(valueToSetAtLocation);
            Assert.That(hasRemovedNumber, Is.EqualTo(false));
            Assert.That(grid.GetPossibilitiesAt(associatedLocation).Count, Is.EqualTo(8));
        }
        
    }
}