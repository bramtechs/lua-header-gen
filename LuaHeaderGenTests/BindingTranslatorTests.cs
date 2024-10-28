﻿using LuaHeaderGenLib;
using LuaHeaderGenLib.Domain;

namespace LuaHeaderGenTests;

public class BindingTranslatorTests
{
    [Test]
    public void TestSimpleTranslation()
    {
        string header = StringUtils.TrimMultiline("""
            void DrawRectangle(int x, int y, int w, int h, Color color);
            """);

        string luaCode = StringUtils.TrimMultiline("""
            --- @param x number
            --- @param y number
            --- @param w number
            --- @param h number
            --- @param color Color
            function DrawRectangle(x, y, w, h, color)
            end
            """);

        Binding binding = BindingBuilder.Build(header);
        string actualLuaCode = new BindingTranslator(binding).Translate();
        Assert.That(actualLuaCode, Is.EqualTo(luaCode));
    }

    [Test]
    public void TestDocumentParameter()
    {
        Assert.Multiple(() =>
        {
            Assert.That(BindingTranslator.DocumentParameter("int", "x"), Is.EqualTo("--- @param x number"));
            Assert.That(BindingTranslator.DocumentParameter("Color", "color"), Is.EqualTo("--- @param color Color"));
        });
    }

    [Test]
    public void TestTranslateTypes()
    {
        Assert.Multiple(() =>
        {
            Assert.That(BindingTranslator.TranslateType("int"), Is.EqualTo("number"), "int should translate to number");
            Assert.That(BindingTranslator.TranslateType("Color"), Is.EqualTo("Color"), "Color should remain Color");
            Assert.That(BindingTranslator.TranslateType("float"), Is.EqualTo("number"), "float should translate to number");
            Assert.That(BindingTranslator.TranslateType("double"), Is.EqualTo("number"), "double should translate to number");
        });
    }

    [Test]
    public void TestTranslateFunction()
    {
        Assert.That(BindingTranslator.TranslateFunction("DrawRectangle", [("int", "posX"), ("int", "posY"), ("int", "width"), ("int", "height"), ("Color", "color")]),
            Is.EqualTo("function DrawRectangle(posX, posY, width, height, color)\nend"));
    }

    [Test]
    public void TestTranslateFunctionFull()
    {
        string header = "void DrawRectangle(int posX, int posY, int width, int height, Color color);";
        Binding binding = BindingBuilder.Build(header);
        Assert.That(new BindingTranslator(binding).TranslateFunction(), Is.EqualTo("function DrawRectangle(posX, posY, width, height, color)\nend"));
    }
}