namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

/// <summary>
/// Represents the square map lines type.
/// </summary>
public class SquareMapLines
{
    /// <summary>
    /// Gets or sets the curly brackets.
    /// </summary>
    public Dictionary<int, List<int>> CurlyBrackets { get; set; } = null!;

    /// <summary>
    /// Gets or sets the square brackets.
    /// </summary>
    public Dictionary<int, List<int>> SquareBrackets { get; set; } = null!;

    /// <summary>
    /// Gets or sets the brackets.
    /// </summary>
    public Dictionary<int, List<int>> Brackets { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ending curly brackets.
    /// </summary>
    public Dictionary<int, List<int>> EndingCurlyBrackets { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ending square brackets.
    /// </summary>
    public Dictionary<int, List<int>> EndingSquareBrackets { get; set; } = null!;

    /// <summary>
    /// Gets or sets the ending brackets.
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
    /// Square map lines.
    /// </summary>
    /// <param name="curlyBracketCapacity">The curly bracket capacity.</param>
    /// <param name="squareBracketCapacity">The square bracket capacity.</param>
    /// <param name="bracketCapacity">The bracket capacity.</param>
    /// <returns>The square map lines result.</returns>
    public SquareMapLines(SquareMap squareMap)
    {
        Init(squareMap.CurlyBrackets.Count, squareMap.SquareBrackets.Count, squareMap.Brackets.Count);
    }

    /// <summary>
    /// Square map lines.
    /// </summary>
    /// <param name="curlyBracketCapacity">The curly bracket capacity.</param>
    /// <param name="squareBracketCapacity">The square bracket capacity.</param>
    /// <param name="bracketCapacity">The bracket capacity.</param>
    /// <returns>The square map lines result.</returns>
    public SquareMapLines()
    {
        Init(0, 0, 0);
    }

    /// <summary>
    /// Add.
    /// </summary>
    /// <param name="curlyBracketCapacity">The curly bracket capacity.</param>
    /// <returns>The add result.</returns>
    public void Add(Enums.Brackets bracketType, bool isEnding, int index, int line)
    {
        Dictionary<int, List<int>>? targetDictionary = null;

        switch (bracketType)
        {
            case Enums.Brackets.Curly:
                targetDictionary = isEnding ? EndingCurlyBrackets : CurlyBrackets;
                break;
            case Enums.Brackets.Square:
                targetDictionary = isEnding ? EndingSquareBrackets : SquareBrackets;
                break;
            case Enums.Brackets.Normal:
                targetDictionary = isEnding ? EndingBrackets : Brackets;
                break;
        }

        if (targetDictionary is not null)
        {
            if (!targetDictionary.ContainsKey(line))
            {
                targetDictionary[line] = new List<int>();
            }
            targetDictionary[line].Add(index);
        }
    }
}
