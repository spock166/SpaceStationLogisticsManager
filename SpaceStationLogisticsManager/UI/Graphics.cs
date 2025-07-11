using Terminal.Gui;

namespace SpaceStationLogisticsManager.UI
{
    /// <summary>
    /// Provides graphical utilities for drawing shapes and text in Terminal.Gui windows.
    /// </summary>
    public static class Graphics
    {
        /// <summary>
        /// Adds a circle to the specified window.
        /// </summary>
        /// <param name="window">The window to draw the circle in.</param>
        /// <param name="h">The x-coordinate of the circle's center.</param>
        /// <param name="k">The y-coordinate of the circle's center.</param>
        /// <param name="radius">The radius of the circle.</param>
        public static void AddCircleToWindow(Window window, int h, int k, float radius)
        {
            int minX = (int)Math.Floor(h - radius);
            int maxX = (int)Math.Ceiling(h + radius);
            int minY = (int)Math.Floor(k - radius);
            int maxY = (int)Math.Ceiling(k + radius);

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    if (Math.Abs((x - h) * (x - h) + (y - k) * (y - k) - radius * radius) <= 4)
                    {
                        AddCharacterToWindow(window, x, y, '·');
                    }
                }
            }
        }

        /// <summary>
        /// Adds a line to the specified window.
        /// </summary>
        /// <param name="window">The window to draw the line in.</param>
        /// <param name="startX">The x-coordinate of the line's start point.</param>
        /// <param name="startY">The y-coordinate of the line's start point.</param>
        /// <param name="endX">The x-coordinate of the line's end point.</param>
        /// <param name="endY">The y-coordinate of the line's end point.</param>
        public static void AddLineToWindow(Window window, int startX, int startY, int endX, int endY)
        {
            if (startX == endX && startY == endY)
            {
                AddPointToWindow(window, startX, startY);
                return;
            }

            if (startX == endX)
            {
                AddVerticalLineToWindow(window, startX, startY, endY);
                return;
            }

            if (endX == endY)
            {
                AddHorizontalLineToWindow(window, startY, startX, endX);
                return;
            }

            int largerX = Math.Max(startX, endX);
            int largerY = Math.Max(startY, endY);
            int smallerX = Math.Min(startX, endX);
            int smallerY = Math.Min(startY, endY);

            float slope = (float)(endY - startY) / (endX - startX);
            char charToUse = '-';

            if (Math.Abs(slope) > 2.5)
            {
                charToUse = '|';
            }
            else if (slope > 0.3)
            {
                charToUse = '\\';
            }
            else if (slope < -0.3)
            {
                charToUse = '/';
            }

            for (int x = smallerX; x <= largerX; x++)
            {
                for (int y = smallerY; y <= largerY; y++)
                {
                    if (Math.Abs(y - (slope * (x - startX) + startY)) <= 0.5)
                    {
                        AddCharacterToWindow(window, x, y, charToUse);
                    }
                }
            }
        }

        /// <summary>
        /// Adds a point to the specified window.
        /// </summary>
        /// <param name="window">The window to draw the point in.</param>
        /// <param name="x">The x-coordinate of the point.</param>
        /// <param name="y">The y-coordinate of the point.</param>
        public static void AddPointToWindow(Window window, int x, int y)
        {
            AddCharacterToWindow(window, x, y, '●');
        }

        /// <summary>
        /// Adds a vertical line to the specified window.
        /// </summary>
        /// <param name="window">The window to draw the line in.</param>
        /// <param name="x">The x-coordinate of the line.</param>
        /// <param name="startY">The y-coordinate of the line's start point.</param>
        /// <param name="endY">The y-coordinate of the line's end point.</param>
        public static void AddVerticalLineToWindow(Window window, int x, int startY, int endY)
        {
            if (startY > endY)
            {
                (startY, endY) = (endY, startY);
            }

            for (int y = startY; y <= endY; y++)
            {
                AddCharacterToWindow(window, x, y, '|');
            }
        }

        /// <summary>
        /// Adds a horizontal line to the specified window.
        /// </summary>
        /// <param name="window">The window to draw the line in.</param>
        /// <param name="y">The y-coordinate of the line.</param>
        /// <param name="startX">The x-coordinate of the line's start point.</param>
        /// <param name="endX">The x-coordinate of the line's end point.</param>
        public static void AddHorizontalLineToWindow(Window window, int y, int startX, int endX)
        {
            if (startX > endX)
            {
                (startX, endX) = (endX, startX);
            }

            for (int x = startX; x <= endX; x++)
            {
                AddCharacterToWindow(window, y, x, '-');
            }
        }

        /// <summary>
        /// Adds a string to the specified window.
        /// </summary>
        /// <param name="window">The window to draw the string in.</param>
        /// <param name="x">The x-coordinate of the string's start point.</param>
        /// <param name="y">The y-coordinate of the string's start point.</param>
        /// <param name="str">The string to draw.</param>
        public static void AddStringToWindow(Window window, int x, int y, string str)
        {
            for (int index = 0; index < str.Length; index++)
            {
                AddCharacterToWindow(window, x + index, y, str[index]);
            }
        }

        /// <summary>
        /// Adds a character to the specified window.
        /// </summary>
        /// <param name="window">The window to draw the character in.</param>
        /// <param name="x">The x-coordinate of the character.</param>
        /// <param name="y">The y-coordinate of the character.</param>
        /// <param name="c">The character to draw.</param>
        private static void AddCharacterToWindow(Window window, int x, int y, char c)
        {
            window.Add(new Label(c.ToString()) { X = x, Y = y });
        }
    }
}

