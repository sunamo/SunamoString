// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoString._public.SunamoData.Data;


public class SquareMap
{
    public List<int> CurlyBrackets { get; set; } = new List<int>();
    public List<int> SquareBrackets { get; set; } = new List<int>();
    public List<int> Brackets { get; set; } = new List<int>();
    public List<int> EndingCurlyBrackets { get; set; } = new List<int>();
    public List<int> EndingSquareBrackets { get; set; } = new List<int>();
    public List<int> EndingBrackets { get; set; } = new List<int>();
#pragma warning disable
    public void Add(Object bracketType, bool end, int i)
    {
    }
#pragma warning restore
}