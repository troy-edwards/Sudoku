namespace Library;

public class BoardFromApi
{
    public Newboard Newboard { get; set; } = new Newboard();
}

public class Newboard
{
    public List<Grid> Grids { get; set; } = new List<Grid>();
    public int Results { get; set; }
    public string Message { get; set; } = "";
}

public class Grid
{
    public List<List<int>> Value { get; set; } = new List<List<int>>();
    public List<List<int>> Solution { get; set; } = new List<List<int>>();
    public string Difficulty { get; set; } = "";
}

