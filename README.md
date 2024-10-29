# Lua Header Generator

Convert C++/C files to Lua header files.

TODO:
- [ ] Document return type
- [ ] Include code comments

## Current functionality

```c
RLAPI void DrawRectangle(int posX, int posY, int width, int height, Color color);                        // Draw a color-filled rectangle
RLAPI void DrawRectangleV(Vector2 position, Vector2 size, Color color);                                  // Draw a color-filled rectangle (Vector version)
RLAPI void DrawRectangleRec(Rectangle rec, Color color);                                                 // Draw a color-filled rectangle
RLAPI void DrawRectanglePro(Rectangle rec, Vector2 origin, float rotation, Color color);                 // Draw a color-filled rectangle with pro parameters
RLAPI void DrawRectangleGradientV(int posX, int posY, int width, int height, Color top, Color bottom);   // Draw a vertical-gradient-filled rectangle
RLAPI void DrawRectangleGradientH(int posX, int posY, int width, int height, Color left, Color right);   // Draw a horizontal-gradient-filled rectangle
RLAPI void DrawRectangleGradientEx(Rectangle rec, Color topLeft, Color bottomLeft, Color topRight, Color bottomRight); // Draw a gradient-filled rectangle with custom vertex colors
RLAPI void DrawRectangleLines(int posX, int posY, int width, int height, Color color);                   // Draw rectangle outline
RLAPI void DrawRectangleLinesEx(Rectangle rec, float lineThick, Color color);                            // Draw rectangle outline with extended parameters
```
turns into
```lua
--- @param posX number
--- @param posY number
--- @param width number
--- @param height number
--- @param color Color
function DrawRectangle(posX, posY, width, height, color)
end

--- @param position Vector2
--- @param size Vector2
--- @param color Color
function DrawRectangleV(position, size, color)
end

--- @param rec Rectangle
--- @param color Color
function DrawRectangleRec(rec, color)
end

--- @param rec Rectangle
--- @param origin Vector2
--- @param rotation number
--- @param color Color
function DrawRectanglePro(rec, origin, rotation, color)
end

--- @param posX number
--- @param posY number
--- @param width number
--- @param height number
--- @param top Color
--- @param bottom Color
function DrawRectangleGradientV(posX, posY, width, height, top, bottom)
end

--- @param posX number
--- @param posY number
--- @param width number
--- @param height number
--- @param left Color
--- @param right Color
function DrawRectangleGradientH(posX, posY, width, height, left, right)
end

--- @param rec Rectangle
--- @param topLeft Color
--- @param bottomLeft Color
--- @param topRight Color
--- @param bottomRight Color
function DrawRectangleGradientEx(rec, topLeft, bottomLeft, topRight, bottomRight)
end

--- @param posX number
--- @param posY number
--- @param width number
--- @param height number
--- @param color Color
function DrawRectangleLines(posX, posY, width, height, color)
end

--- @param rec Rectangle
--- @param lineThick number
--- @param color Color
function DrawRectangleLinesEx(rec, lineThick, color)
end
```