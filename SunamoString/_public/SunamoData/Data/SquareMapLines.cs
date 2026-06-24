namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

public class SquareMapLines
{
    public Dictionary<int, List<int>> CurlyBrackets { get; set; } = null!;

    public Dictionary<int, List<int>> SquareBrackets { get; set; } = null!;

    public Dictionary<int, List<int>> Brackets { get; set; } = null!;

    public Dictionary<int, List<int>> EndingCurlyBrackets { get; set; } = null!;

    public Dictionary<int, List<int>> EndingSquareBrackets { get; set; } = null!;

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

    public SquareMapLines(SquareMap squareMap)
    {
        Init(squareMap.CurlyBrackets.Count, squareMap.SquareBrackets.Count, squareMap.Brackets.Count);
    }

    public SquareMapLines()
    {
        Init(0, 0, 0);
    }

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
