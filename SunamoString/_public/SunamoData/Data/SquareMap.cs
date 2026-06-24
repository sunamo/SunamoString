namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

public class SquareMap
{
    public List<int> CurlyBrackets { get; set; } = new List<int>();

    public List<int> SquareBrackets { get; set; } = new List<int>();

    public List<int> Brackets { get; set; } = new List<int>();

    public List<int> EndingCurlyBrackets { get; set; } = new List<int>();

    public List<int> EndingSquareBrackets { get; set; } = new List<int>();

    public List<int> EndingBrackets { get; set; } = new List<int>();

    public void Add(Enums.Brackets bracketType, bool isEnding, int index)
    {
        switch (bracketType)
        {
            case Enums.Brackets.Curly:
                if (isEnding)
                    EndingCurlyBrackets.Add(index);
                else
                    CurlyBrackets.Add(index);
                break;
            case Enums.Brackets.Square:
                if (isEnding)
                    EndingSquareBrackets.Add(index);
                else
                    SquareBrackets.Add(index);
                break;
            case Enums.Brackets.Normal:
                if (isEnding)
                    EndingBrackets.Add(index);
                else
                    Brackets.Add(index);
                break;
        }
    }
}
