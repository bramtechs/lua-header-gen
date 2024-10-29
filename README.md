# Lua Header Generator

Convert C++/C header files to Lua headers. For supporting linting and autocompletion of custom bindings in text editors.

> Internal tool, work in progress.

## Current functionality

raylib.h:
```c
RLAPI void DrawRectangle(int posX, int posY, int width, int height, Color color);                        // Draw a color-filled rectangle
RLAPI void DrawRectangleV(Vector2 position, Vector2 size, Color color);                                  // Draw a color-filled rectangle (Vector version)
RLAPI void DrawRectangleRec(Rectangle rec, Color color);                                                 // Draw a color-filled rectangle
RLAPI void DrawRectanglePro(Rectangle rec, Vector2 origin, float rotation, Color color);                 // Draw a color-filled rectangle with pro parameters
// -- snip --
RLAPI void SetTextLineSpacing(int spacing);                                                              // Set vertical line spacing when drawing with line-breaks
RLAPI int MeasureText(const char *text, int fontSize);                                                   // Measure string width for default font
RLAPI Vector2 MeasureTextEx(Font font, const char *text, float fontSize, float spacing);                 // Measure string size for Font
RLAPI int GetGlyphIndex(Font font, int codepoint);                                                       // Get glyph index position in font for a codepoint (unicode character), fallback to '?' if not found
RLAPI GlyphInfo GetGlyphInfo(Font font, int codepoint);                                                  // Get glyph font info data for a codepoint (unicode character), fallback to '?' if not found
RLAPI Rectangle GetGlyphAtlasRec(Font font, int codepoint);                                              // Get glyph rectangle in font atlas for a codepoint (unicode character), fallback to '?' if not found
```
turns into raylib.lua:
```lua
--- Draw a color-filled rectangle
--- @param posX number
--- @param posY number
--- @param width number
--- @param height number
--- @param color Color
--- @return nil
function DrawRectangle(posX, posY, width, height, color)
end

--- Draw a color-filled rectangle (Vector version)
--- @param position Vector2
--- @param size Vector2
--- @param color Color
--- @return nil
function DrawRectangleV(position, size, color)
end

--- Draw a color-filled rectangle
--- @param rec Rectangle
--- @param color Color
--- @return nil
function DrawRectangleRec(rec, color)
end

--- Draw a color-filled rectangle with pro parameters
--- @param rec Rectangle
--- @param origin Vector2
--- @param rotation number
--- @param color Color
--- @return nil
function DrawRectanglePro(rec, origin, rotation, color)
end

// -- snip --

--- Set vertical line spacing when drawing with line-breaks
--- @param spacing number
--- @return nil
function SetTextLineSpacing(spacing)
end

--- Measure string width for default font
--- @param text string
--- @param fontSize number
--- @return number
function MeasureText(text, fontSize)
end

--- Measure string size for Font
--- @param font Font
--- @param text string
--- @param fontSize number
--- @param spacing number
--- @return Vector2
function MeasureTextEx(font, text, fontSize, spacing)
end

--- Get glyph index position in font for a codepoint (unicode character), fallback to '?' if not found
--- @param font Font
--- @param codepoint number
--- @return number
function GetGlyphIndex(font, codepoint)
end

--- Get glyph font info data for a codepoint (unicode character), fallback to '?' if not found
--- @param font Font
--- @param codepoint number
--- @return GlyphInfo
function GetGlyphInfo(font, codepoint)
end

--- Get glyph rectangle in font atlas for a codepoint (unicode character), fallback to '?' if not found
--- @param font Font
--- @param codepoint number
--- @return Rectangle
function GetGlyphAtlasRec(font, codepoint)
end
```
