namespace SunamoString._public.SunamoData.Data;







public class TextFormatDataString : List<CharFormatDataString>
{
    
    
    
    
    public int requiredLength = -1;
    public bool trimBefore = false;
    public static class Templates
    {
    }
    
    
    
    
    
    
    public TextFormatDataString(bool trimBefore, int requiredLength, params CharFormatDataString[] a)
    {
        this.trimBefore = trimBefore;
        this.requiredLength = requiredLength;
        AddRange(a);
    }
}