namespace Library;

public static class CellHelpers
{
    public static List<string> GetPossibilitiesDisplay(HashSet<int> allOptions, HashSet<int> possibilities, int blockSize)
    {
        List<string> row = new List<string>();
        List<string> rowStrings = new List<string>();
        int column = 0;
        foreach (var option in allOptions)
        {
            row.Add(possibilities.Contains(option) ? option.ToString() : " ");
            column++;
            if (column % blockSize == 0)
            {
                rowStrings.Add(string.Join(" ", row));
                row.Clear();
                column = 0;
            }
        }

        return rowStrings;
    }
}