namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

/// <summary>
/// Stores positions of brackets per line in a multi-line text, including both opening and closing positions.
/// </summary>
public class SquareMapLines
{
    /// <summary>
    /// Positions of opening curly braces per line.
    /// </summary>
    public Dictionary<int, List<int>> CurlyBrackets { get; set; } = null!;

    /// <summary>
    /// Positions of opening square brackets per line.
    /// </summary>
    public Dictionary<int, List<int>> SquareBrackets { get; set; } = null!;

    /// <summary>
    /// Positions of opening parentheses per line.
    /// </summary>
    public Dictionary<int, List<int>> Brackets { get; set; } = null!;

    /// <summary>
    /// Positions of closing curly braces per line.
    /// </summary>
    public Dictionary<int, List<int>> EndingCurlyBrackets { get; set; } = null!;

    /// <summary>
    /// Positions of closing square brackets per line.
    /// </summary>
    public Dictionary<int, List<int>> EndingSquareBrackets { get; set; } = null!;

    /// <summary>
    /// Positions of closing parentheses per line.
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
    /// Initializes a new instance from an existing <see cref="SquareMap"/> with matching capacities.
    /// </summary>
    /// <param name="squareMap">The square map to use for capacity initialization.</param>
    public SquareMapLines(SquareMap squareMap)
    {
        Init(squareMap.CurlyBrackets.Count, squareMap.SquareBrackets.Count, squareMap.Brackets.Count);
    }

    /// <summary>
    /// Initializes a new empty instance with zero capacity.
    /// </summary>
    public SquareMapLines()
    {
        Init(0, 0, 0);
    }

    /// <summary>
    /// Adds a bracket position to the appropriate dictionary based on bracket type, direction, and line.
    /// </summary>
    /// <param name="bracketType">The type of bracket (curly, square, or normal).</param>
    /// <param name="isEnding">Whether this is a closing bracket.</param>
    /// <param name="index">The position index in the line.</param>
    /// <param name="line">The line number.</param>
    public void Add(Enums.Brackets bracketType, bool isEnding, int index, int line)
    {
        Dictionary<int, List<int>>? targetDictionary = null;

        switch (bracketType)
        {
            case Enums.Brackets.Curly:
                targetDictionary = isEnding ? this.EndingCurlyBrackets : this.CurlyBrackets;
                break;
            case Enums.Brackets.Square:
                targetDictionary = isEnding ? this.EndingSquareBrackets : this.SquareBrackets;
                break;
            case Enums.Brackets.Normal:
                targetDictionary = isEnding ? this.EndingBrackets : this.Brackets;
                break;
        }

        if (targetDictionary != null)
        {
            if (!targetDictionary.ContainsKey(line))
            {
                targetDictionary[line] = new List<int>();
            }
            targetDictionary[line].Add(index);
        }
    }
}
