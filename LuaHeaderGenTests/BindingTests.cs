namespace LuaHeaderGenTests;

using LuaHeaderGenLib;
using LuaHeaderGenLib.Domain;
using System.Security.Cryptography.X509Certificates;

internal class BindingTests
{
    [Test]
    public void TestSimpleBinding1()
    {
        Binding binding = BindingBuilder.Build("int test()");
        Assert.That(binding.ReturnType, Is.EqualTo("int"));
        Assert.That(binding.Name, Is.EqualTo("test"));
        Assert.That(binding.Comment, Is.EqualTo(""));
        Assert.That(binding.ReturnType, Is.EqualTo("int"));
    }

    [Test]
    public void TestSimpleBinding2()
    {
        Binding binding = BindingBuilder.Build("void lua_test()");
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
        Assert.That(binding.Name, Is.EqualTo("lua_test"));
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
    }

    [Test]
    public void TestSimpleBinding2Fuzzed()
    {
        Binding binding = BindingBuilder.Build("void  lua_test ()");
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
        Assert.That(binding.Name, Is.EqualTo("lua_test"));
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
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
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
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
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
    }

    [Test]
    public void TestVariadicBinding()
    {
        Binding binding = BindingBuilder.Build("void TraceLog(int logLevel, const char *  text, ...);         // Show trace log messages (LOG_DEBUG, LOG_INFO, LOG_WARNING, LOG_ERROR...)");
        Assert.That(binding.GetArgumentType("logLevel"), Is.EqualTo("int"));
        Assert.That(binding.GetArgumentType("text"), Is.EqualTo("char*"));
        Assert.That(binding.Comment, Is.EqualTo("Show trace log messages (LOG_DEBUG, LOG_INFO, LOG_WARNING, LOG_ERROR...)"));
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
    }

    [Test]
    public void TestBindingWithMacro()
    {
        Binding binding = BindingBuilder.Build("bool ExportWave(Wave wave, const char *fileName);");
        Assert.That(binding.GetArgumentType("wave"), Is.EqualTo("Wave"));
        Assert.That(binding.GetArgumentType("fileName"), Is.EqualTo("char*"));
        Assert.That(binding.ReturnType, Is.EqualTo("bool"));
    }

    [Test]
    public void TestBindingWithOneParameter()
    {
        Binding binding = BindingBuilder.Build("Model LoadModel(Model model);");
        Assert.That(binding.GetArgumentType("model"), Is.EqualTo("Model"));
        Assert.That(binding.ReturnType, Is.EqualTo("Model"));
    }

    [Test]
    public void TestBindingWithVoidParameter()
    {
        Binding binding = BindingBuilder.Build("void CloseWindow(void);");
        Assert.That(binding.Arguments.Count, Is.EqualTo(0));
        Assert.That(binding.ReturnType, Is.EqualTo("void"));
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