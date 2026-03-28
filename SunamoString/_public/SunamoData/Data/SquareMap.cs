namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

/// <summary>
/// Stores positions of brackets (curly, square, normal) found in a text, including both opening and closing positions.
/// </summary>
public class SquareMap
{
    /// <summary>
    /// Positions of opening curly braces.
    /// </summary>
    public List<int> CurlyBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Positions of opening square brackets.
    /// </summary>
    public List<int> SquareBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Positions of opening parentheses.
    /// </summary>
    public List<int> Brackets { get; set; } = new List<int>();

    /// <summary>
    /// Positions of closing curly braces.
    /// </summary>
    public List<int> EndingCurlyBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Positions of closing square brackets.
    /// </summary>
    public List<int> EndingSquareBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Positions of closing parentheses.
    /// </summary>
    public List<int> EndingBrackets { get; set; } = new List<int>();

    /// <summary>
    /// Adds a bracket position to the appropriate list based on bracket type and direction.
    /// </summary>
    /// <param name="bracketType">The type of bracket (curly, square, or normal).</param>
    /// <param name="isEnding">Whether this is a closing bracket.</param>
    /// <param name="index">The position index in the text.</param>
    public void Add(Enums.Brackets bracketType, bool isEnding, int index)
    {
        switch (bracketType)
        {
            case Enums.Brackets.Curly:
                if (isEnding)
                    this.EndingCurlyBrackets.Add(index);
                else
                    this.CurlyBrackets.Add(index);
                break;
            case Enums.Brackets.Square:
                if (isEnding)
                    this.EndingSquareBrackets.Add(index);
                else
                    this.SquareBrackets.Add(index);
                break;
            case Enums.Brackets.Normal:
                if (isEnding)
                    this.EndingBrackets.Add(index);
                else
                    this.Brackets.Add(index);
                break;
        }
    }
}
