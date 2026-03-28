namespace SunamoString._sunamo.SunamoNumbers;

/// <summary>
/// Number helper providing methods for joining numeric tokens.
/// </summary>
internal class NH
{
    /// <summary>
    /// Joins consecutive numeric tokens starting from the specified index into a single string.
    /// </summary>
    /// <param name="list">The list of string tokens.</param>
    /// <param name="startIndex">The index to start joining from.</param>
    internal static string JoinAnotherTokensIfIsNumber(List<string> list, int startIndex)
    {
        StringBuilder stringBuilder = new StringBuilder();

        for (; startIndex < list.Count; startIndex++)
        {
            if (int.TryParse(list[startIndex], out var _))
            {
                stringBuilder.Append(list[startIndex]);
            }
            else
            {
                break;
            }
        }

        return stringBuilder.ToString();
    }
}
