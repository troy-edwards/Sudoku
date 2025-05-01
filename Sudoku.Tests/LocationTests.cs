using Library;

namespace Sudoku.Tests;

[TestFixture]
public class LocationTests
{
    [Test]
    public void GetChildLocationTest()
    {
        int parentSize = 3;
        Location parent = new Location(2, 2);
        Location child = new Location(2, 2);
        Location expectedLocationOnGrid = new Location(8, 8);

        var actualChildLocationOnGrid = parent.GetChildLocation(child, parentSize);
        
        Assert.That(actualChildLocationOnGrid, Is.EqualTo(expectedLocationOnGrid));
    }
    
    [Test]
    public void GetChildLocationWithRowColumnTest()
    {
        int parentSize = 3;
        Location parent = new Location(2, 2);
        Location expectedLocationOnGrid = new Location(8, 8);

        var actualChildLocationOnGrid = parent.GetChildLocation(2, 2, parentSize);
        
        Assert.That(actualChildLocationOnGrid, Is.EqualTo(expectedLocationOnGrid));
    }

    [Test]
    public void GetParentBlockTest()
    {
        Location childLocation = new Location(4, 6);
        Location expectedParentLocation = new Location(1, 2);
        int parentSize = 3;

        var actualLocation = childLocation.GetParentBlock(parentSize);
        
        Assert.That(actualLocation, Is.EqualTo(expectedParentLocation));
    }

    [Test]
    public void GetLocationsInSameRowReturnsAllInRowButSelf()
    {
        int size = 9;
        Location location = new Location(3, 6);
        
        HashSet<Location> expectedLocations =
        [
            new Location(3, 0),
            new Location(3, 1),
            new Location(3, 2),
            new Location(3, 3),
            new Location(3, 4),
            new Location(3, 5),
            // not 6
            new Location(3, 7),
            new Location(3, 8)
        ];

        var actual = location.GetOtherLocationsInSameRow(size);
        
        Assert.That(actual, Is.EquivalentTo(expectedLocations));
    }    
    [Test]
    public void GetLocationsInSameColumnReturnsAllInRowButSelf()
    {
        int size = 9;
        Location location = new Location(3, 6);
        
        HashSet<Location> expectedLocations =
        [
            new Location(0, 6),
            new Location(1, 6),
            new Location(2, 6),
            // not 3, 6
            new Location(4, 6),
            new Location(5, 6),
            new Location(6, 6),
            new Location(7, 6),
            new Location(8, 6)
        ];

        var actual = location.GetOtherLocationsInSameColumn(size);
        
        Assert.That(actual, Is.EquivalentTo(expectedLocations));
    }

    [Test]
    public void GetLocationsInSameBlockReturnsAllInBlockButSelf()
    {
        int gridSize = 9;
        Location location = new Location(3, 6);
        HashSet<Location> expectedLocations =
        [
            // Not 3,6
            new Location(3, 7),
            new Location(3, 8),
                        
            new Location(4, 6),
            new Location(4, 7),
            new Location(4, 8),
                        
            new Location(5, 6),
            new Location(5, 7),
            new Location(5, 8),
        ];

        var actual = location.GetOtherLocationsInSameBlock(gridSize);
        
        Assert.That(actual, Is.EquivalentTo(expectedLocations));
    }

    [Test]
    public void GetAffectedLocationsGivesAllOthersInRowColumnAndBlock()
    {
        int gridSize = 9;
        Location location = new Location(4, 1);
        HashSet<Location> expectedLocations =
        [
            // block
            new Location(3, 0),
            new Location(3, 1),
            new Location(3, 2),
            
            new Location(4, 0),
            // Not 4,1
            new Location(4, 2),
            
            new Location(5, 0),
            new Location(5, 1),
            new Location(5, 2),
            
            // Row
            new Location(4, 3),
            new Location(4, 4),
            new Location(4, 5),
            new Location(4, 6),
            new Location(4, 7),
            new Location(4, 8),
            
            // Col
            new Location(0, 1),
            new Location(1, 1),
            new Location(2, 1),
            new Location(6, 1),
            new Location(7, 1),
            new Location(8, 1),
        ];

        var actual = location.GetAssociatedLocations(gridSize);
    }
}