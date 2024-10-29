using LuaHeaderGenLib;

namespace LuaHeaderGenTests;

internal class ConfigResolverTests
{
    [Test]
    public void TestEnsurePrefixDot()
    {
        Assert.Multiple(() =>
        {
            Assert.That(ConfigResolver.EnsurePrefixDot(".jpg"), Is.EqualTo(".jpg"));
            Assert.That(ConfigResolver.EnsurePrefixDot("PNG"), Is.EqualTo(".PNG"));
            Assert.That(ConfigResolver.EnsurePrefixDot(".GIF"), Is.EqualTo(".GIF"));
        });
    }

    [Test]
    public void TestSanitizeExtensions()
    {
        Assert.That(ConfigResolver.SanitizeExtensions([".jpg", "PNG", "cc", ".CPP", ".CPP"]),
            Is.EquivalentTo(new HashSet<string>([".jpg", ".png", ".cc", ".cpp"])));
    }
}

