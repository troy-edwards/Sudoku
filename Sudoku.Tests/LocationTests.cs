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
}