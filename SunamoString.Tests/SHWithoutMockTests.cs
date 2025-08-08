namespace sunamo.Tests.Helpers.Text;
public class SHWithoutMockTests
{
    //[Fact]
    public
#if ASYNC
async Task
#else
    void
#endif
ReplaceManyFromStringTest()
    {
        //            string testString = @"Assert.Equal -> Assert.AreEqual
        //Assert.AreEqual<*> -> CollectionAssert.AreEqual
        //[Fact] -> [Fact]
        //            testString = "Assert.AreEqual<*> -> CollectionAssert.AreEqual";

        //            string file = @"E:\vs\Projects\PlatformIndependentNuGetPackages.Tests\sunamo.Tests.Data\ReplaceManyFromString\In_ReplaceManyFromString.cs";
        //            var s =
        //#if ASYNC
        //    await
        //#endif
        // TF.ReadAllText(file);

        //            s = SHReplace.ReplaceManyFromString(s, testString, Consts.transformTo);


        //#if ASYNC
        //            await
        //#endif
        //            TF.WriteAllText(file, s);
    }
}
