// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._public.SunamoData.Data;


public class SquareMapLines
{
    public Dictionary<int, List<int>> CurlyBrackets { get; set; }
    public Dictionary<int, List<int>> SquareBrackets { get; set; }
    public Dictionary<int, List<int>> Brackets { get; set; }
    public Dictionary<int, List<int>> EndingCurlyBrackets { get; set; }
    public Dictionary<int, List<int>> EndingSquareBrackets { get; set; }
    public Dictionary<int, List<int>> EndingBrackets { get; set; }
    void Init(int curlyBracketCapacity, int squareBracketCapacity, int bracketCapacity)
    {
        CurlyBrackets = new Dictionary<int, List<int>>(curlyBracketCapacity);
        SquareBrackets = new Dictionary<int, List<int>>(squareBracketCapacity);
        Brackets = new Dictionary<int, List<int>>(bracketCapacity);
        EndingCurlyBrackets = new Dictionary<int, List<int>>(curlyBracketCapacity);
        EndingSquareBrackets = new Dictionary<int, List<int>>(squareBracketCapacity);
        EndingBrackets = new Dictionary<int, List<int>>(bracketCapacity);
    }
    public SquareMapLines(SquareMap squareMap)
    {
        Init(squareMap.CurlyBrackets.Count, squareMap.SquareBrackets.Count, squareMap.Brackets.Count);
    }
    public SquareMapLines()
    {
        Init(0, 0, 0);
    }






#pragma warning disable
    public void Add(Object bracketType, bool end, int i, int line)
    {
    }
#pragma warning restore
}