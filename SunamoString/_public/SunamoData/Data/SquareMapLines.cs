namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class SquareMapLines
{
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public Dictionary<int, List<int>> CurlyBrackets { get; set; } = null!;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public Dictionary<int, List<int>> SquareBrackets { get; set; } = null!;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public Dictionary<int, List<int>> Brackets { get; set; } = null!;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public Dictionary<int, List<int>> EndingCurlyBrackets { get; set; } = null!;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public Dictionary<int, List<int>> EndingSquareBrackets { get; set; } = null!;
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public Dictionary<int, List<int>> EndingBrackets { get; set; } = null!;
    void Init(int curlyBracketCapacity, int squareBracketCapacity, int bracketCapacity)
    {
        CurlyBrackets = new Dictionary<int, List<int>>(curlyBracketCapacity);
        SquareBrackets = new Dictionary<int, List<int>>(squareBracketCapacity);
        Brackets = new Dictionary<int, List<int>>(bracketCapacity);
        EndingCurlyBrackets = new Dictionary<int, List<int>>(curlyBracketCapacity);
        EndingSquareBrackets = new Dictionary<int, List<int>>(squareBracketCapacity);
        EndingBrackets = new Dictionary<int, List<int>>(bracketCapacity);
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public SquareMapLines(SquareMap squareMap)
    {
        Init(squareMap.CurlyBrackets.Count, squareMap.SquareBrackets.Count, squareMap.Brackets.Count);
    }
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public SquareMapLines()
    {
        Init(0, 0, 0);
    }






    // EN: Add bracket index to appropriate dictionary based on bracket type, line number, and whether it's an ending bracket
    // CZ: Přidat index závorky do příslušného dictionary podle typu závorky, čísla řádku a zda je to uzavírací závorka
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public void Add(Enums.Brackets bracketType, bool end, int index, int line)
    {
        Dictionary<int, List<int>>? targetDict = null;

        switch (bracketType)
        {
            case Enums.Brackets.Curly:
                targetDict = end ? this.EndingCurlyBrackets : this.CurlyBrackets;
                break;
            case Enums.Brackets.Square:
                targetDict = end ? this.EndingSquareBrackets : this.SquareBrackets;
                break;
            case Enums.Brackets.Normal:
                targetDict = end ? this.EndingBrackets : this.Brackets;
                break;
        }

        if (targetDict != null)
        {
            if (!targetDict.ContainsKey(line))
            {
                targetDict[line] = new List<int>();
            }
            targetDict[line].Add(index);
        }
    }
}
