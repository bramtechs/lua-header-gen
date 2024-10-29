using LuaHeaderGenLib;

namespace LuaHeaderGenTests;

internal class StringUtilsTests
{
    [Test]
    public void RemoveDuplicateSpacing()
    {
        Assert.That(StringUtils.RemoveDuplicateSpacing("a  b   c"), Is.EqualTo("a b c"));
    }

    [Test]
    public void TrimMultiline()
    {
        Assert.That(StringUtils.TrimMultiline("""
            abc
               def
            """), Is.EqualTo("abc\ndef"));
    }
}