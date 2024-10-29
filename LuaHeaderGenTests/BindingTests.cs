namespace LuaHeaderGenTests;

using LuaHeaderGenLib;
using LuaHeaderGenLib.Domain;

internal class BindingTests
{
    [Test]
    public void TestSimpleBinding1()
    {
        Binding binding = BindingBuilder.Build("int test()");
        Assert.That(binding.ReturnType, Is.EqualTo("int"));
        Assert.That(binding.Name, Is.EqualTo("test"));
    }

    [Test]
    public void TestSimpleBinding2()
    {
        Binding binding = BindingBuilder.Build("void lua_test()");
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
        Assert.That(binding.Name, Is.EqualTo("lua_test"));
    }

    [Test]
    public void TestSimpleBinding2Fuzzed()
    {
        Binding binding = BindingBuilder.Build("void  lua_test ()");
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
        Assert.That(binding.Name, Is.EqualTo("lua_test"));
    }

    [Test]
    public void TestSimpleBinding3()
    {
        Binding binding = BindingBuilder.Build("void lua_test(int x, int y, int w, int h, Color color)");
        Assert.That(binding.GetArgumentType("x"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("y"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("w"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("h"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("color"), Is.EqualTo("Color"));
    }

    [Test]
    public void TestSimpleBinding3Fuzzed()
    {
        Binding binding = BindingBuilder.Build("void lua_test(int x ,  int  y ,  int w, int h,  Color color  )");
        Assert.That(binding.GetArgumentType("x"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("y"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("w"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("h"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("color"), Is.EqualTo("Color"));
    }

    [Test]
    public void TestSyntaxThrows()
    {
        AssertUtils.ThrowsAny(() =>
        {
            Binding binding = BindingBuilder.Build("inttest");
        });
    }
}