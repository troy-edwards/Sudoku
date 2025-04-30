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
}