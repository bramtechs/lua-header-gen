namespace LuaHeaderGenTests;

public static class AssertUtils
{
    public static void ThrowsAny(TestDelegate deleg)
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
