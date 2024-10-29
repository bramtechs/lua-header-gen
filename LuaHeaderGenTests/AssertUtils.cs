namespace LuaHeaderGenTests;

internal static class AssertUtils
{
    internal static void ThrowsAny(TestDelegate deleg)
    {
        try
        {
            deleg();
            Assert.Fail("Nothing was thrown");
        }
        catch (Exception)
        {
        }
    }
}