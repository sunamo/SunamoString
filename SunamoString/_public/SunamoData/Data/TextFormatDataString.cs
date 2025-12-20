// variables names: ok
// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._public.SunamoData.Data;







public class TextFormatDataString : List<CharFormatDataString>
{
    
    
    
    
    public int RequiredLength { get; set; } = -1;
    public bool TrimBefore { get; set; } = false;
    public static class Templates
    {
    }
    
    
    
    
    
    
    public TextFormatDataString(bool trimBefore, int requiredLength, params CharFormatDataString[] charFormatDataItems)
    {
        this.TrimBefore = trimBefore;
        this.RequiredLength = requiredLength;
        AddRange(charFormatDataItems);
    }
}
