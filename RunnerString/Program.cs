using SunamoString.Tests2;

namespace RunnerString;

internal class Program
{
    static void Main()
    {
        MainAsync(args).GetAwaiter().GetResult();
    }

    static async Task MainAsync(string[] args)
    {
        SHTests t = new();
        //t.FirstWordWhichIsNumberTest();
        //t.ContainsClTest();
        //t.GetTextBetweenSimpleTest();
        //t.FirstWordWhichIsNumberTest();

        await t.GetTextBetweenSimpleTest2();
    }

}
