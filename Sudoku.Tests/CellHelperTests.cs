﻿using Library;

namespace Sudoku.Tests;

public class CellHelperTests
{
    [Test]
    public void GetPossibilitiesDisplayGivesRows()
    {
        HashSet<int> allOptions = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        HashSet<int> possibilities = [1, 3, 5, 7, 9];
        int size = 3;

        List<string> expected =
        [
            "1   3",
            "  5  ",
            "7   9"
        ];
        
        Assert.That(CellHelpers.GetPossibilitiesDisplay(allOptions, possibilities, size), Is.EqualTo(expected));
        foreach (var row in expected)
        {
            Assert.That(row.Length, Is.EqualTo(5));
        }
    }
}