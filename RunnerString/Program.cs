namespace RunnerString;



internal class Program
{
    static void Main()
    {
        MainAsync().GetAwaiter().GetResult();
    }

    static async Task MainAsync()
    {
        //SHTests t = new();
        ////t.FirstWordWhichIsNumberTest();
        ////t.ContainsClTest();
        ////t.GetTextBetweenSimpleTest();
        ////t.FirstWordWhichIsNumberTest();

        //await t.GetTextBetweenSimpleTest2();
    }

}