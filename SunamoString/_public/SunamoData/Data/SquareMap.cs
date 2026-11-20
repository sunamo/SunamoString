namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

/// <summary>
/// Represents the square map type.
/// </summary>
public class SquareMap
{
    /// <summary>
    /// Gets or sets the curly brackets.
    /// </summary>
    public List<int> CurlyBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Gets or sets the square brackets.
    /// </summary>
    public List<int> SquareBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Gets or sets the brackets.
    /// </summary>
    public List<int> Brackets { get; set; } = new List<int>();

    /// <summary>
    /// Gets or sets the ending curly brackets.
    /// </summary>
    public List<int> EndingCurlyBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Gets or sets the ending square brackets.
    /// </summary>
    public List<int> EndingSquareBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Gets or sets the ending brackets.
    /// </summary>
    public List<int> EndingBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Add.
    /// </summary>
    /// <typeparam name="int">The type int.</typeparam>
    /// <returns>The add result.</returns>
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
