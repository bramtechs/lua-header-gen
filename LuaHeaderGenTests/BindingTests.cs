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
    public void TestVariadicBinding()
    {
        Binding binding = BindingBuilder.Build("void TraceLog(int logLevel, const char *  text, ...);         // Show trace log messages (LOG_DEBUG, LOG_INFO, LOG_WARNING, LOG_ERROR...)");
        Assert.That(binding.GetArgumentType("logLevel"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("text"), Is.EqualTo("char*"));
    }

    [Test]
    public void TestBindingWithOneParameter()
    {
        Binding binding = BindingBuilder.Build("Model LoadModel(Model model);");
        Assert.That(binding.GetArgumentType("model"), Is.EqualTo("Model"));
    }

    [Test]
    public void TestBindingWithVoidParameter()
    {
        Binding binding = BindingBuilder.Build("void CloseWindow(void);");
        Assert.That(binding.GetArguments().Count, Is.EqualTo(0));
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