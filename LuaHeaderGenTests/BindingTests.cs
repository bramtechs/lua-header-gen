namespace LuaHeaderGenTests;
using LuaHeaderGenLib;
public class BindingTests
{
    [Test]
    public void TestSimpleBinding1()
    {
        Binding binding = new("int test()");
        Assert.That(binding.ReturnType, Is.EqualTo("int"));
        Assert.That(binding.Name, Is.EqualTo("test"));
    }

    [Test]
    public void TestSimpleBinding2()
    {
        Binding binding = new("void lua_test()");
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
        Assert.That(binding.Name, Is.EqualTo("lua_test"));
    }

    [Test]
    public void TestSyntaxThrows()
    {
        AssertUtils.ThrowsAny(() =>
        {
            Binding binding = new("inttest");
        });
    }
}