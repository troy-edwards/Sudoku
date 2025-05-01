namespace Library;

public struct Location
{
    public int Row { get; private set; }
    public int Column { get; private set; }

    public Location(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override string ToString()
    {
        return $"[{Row}, {Column}]";
    }


    private sealed class RowColumnEqualityComparer : IEqualityComparer<Location>
    {
        public bool Equals(Location left, Location right)
        {
            return left.Row == right.Row && left.Column == right.Column;
        }

        public int GetHashCode(Location obj)
        {
            return HashCode.Combine(obj.Row, obj.Column);
        }
    }

    public static IEqualityComparer<Location> RowColumnComparer { get; } = new RowColumnEqualityComparer();

    public Location GetChildLocation(Location child, int parentSize)
    {
        int newRow = Row * parentSize + child.Row;
        int newColumn = Column * parentSize + child.Column;

        return new Location(newRow, newColumn);
    }

    public Location GetChildLocation(int childRow, int childColumn, int parentSize)
    {
        return GetChildLocation(new Location(childRow, childColumn), parentSize);
    }

    public Location GetParentBlock(int parentSize)
    {
        return new Location(Row / parentSize, Column/parentSize);
    }

    public HashSet<Location> GetOtherLocationsInSameRow(int gridSize)
    {
        HashSet<Location> others = new HashSet<Location>();
        for (int col = 0; col < gridSize; col++)
        {
            if(col != Column)
                others.Add(new Location(Row, col));
        }

        return others;
    }

    public HashSet<Location> GetOtherLocationsInSameColumn(int gridSize)
    {
        HashSet<Location> others = new HashSet<Location>();
        for (int row = 0; row < gridSize; row++)
        {
            if(row != Row)
                others.Add(new Location(row, Column));
        }

        return others;
    }

    public HashSet<Location> GetOtherLocationsInSameBlock(int gridSize)
    {
        int blockSize = (int)MathF.Sqrt(gridSize);
        HashSet<Location> locations = new HashSet<Location>();
        Location topLeft = GetParentBlock(blockSize).GetChildLocation(new Location(0, 0), blockSize);
        for (int row = topLeft.Row; row < topLeft.Row + blockSize; row++)
        {
            for (int col = topLeft.Column; col < topLeft.Column + blockSize; col++)
            {
                locations.Add(new Location(row, col));
            }
        }

        locations.Remove(this);
        return locations;
    }

    public HashSet<Location> GetAssociatedLocations(int gridSize)
    {
        var combinedSet = GetOtherLocationsInSameBlock(gridSize);
        combinedSet.UnionWith(GetOtherLocationsInSameColumn(gridSize));
        combinedSet.UnionWith(GetOtherLocationsInSameRow(gridSize));
        return combinedSet;
    }
}