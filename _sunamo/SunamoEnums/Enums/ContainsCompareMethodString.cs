namespace SunamoString;


/// <summary>
/// Used in SunamoCollectionsGenericStore + SunamoCollections
/// </summary>
public enum ContainsCompareMethodString
{
    WholeInput,
    SplitToWords,
    /// <summary>
    /// split to words and check for ! at [0]
    /// </summary>
    Negations
}