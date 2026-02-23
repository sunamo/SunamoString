namespace SunamoString._public.SunamoData.Data;

using SunamoString.Enums;

/// <summary>
/// Provides string helper methods for various text operations.
/// </summary>
public class SquareMap
{
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public List<int> CurlyBrackets { get; set; } = new List<int>();
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public List<int> SquareBrackets { get; set; } = new List<int>();
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public List<int> Brackets { get; set; } = new List<int>();
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public List<int> EndingCurlyBrackets { get; set; } = new List<int>();
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public List<int> EndingSquareBrackets { get; set; } = new List<int>();
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public List<int> EndingBrackets { get; set; } = new List<int>();
    // EN: Add bracket index to appropriate list based on bracket type and whether it's an ending bracket
    // CZ: Přidat index závorky do příslušného listu podle typu závorky a zda je to uzavírací závorka
    /// <summary>
    /// Performs an operation.
    /// </summary>
    public void Add(Enums.Brackets bracketType, bool end, int index)
    {
        switch (bracketType)
        {
            case Enums.Brackets.Curly:
                if (end)
                    this.EndingCurlyBrackets.Add(index);
                else
                    this.CurlyBrackets.Add(index);
                break;
            case Enums.Brackets.Square:
                if (end)
                    this.EndingSquareBrackets.Add(index);
                else
                    this.SquareBrackets.Add(index);
                break;
            case Enums.Brackets.Normal:
                if (end)
                    this.EndingBrackets.Add(index);
                else
                    this.Brackets.Add(index);
                break;
        }
    }
}
