namespace ConsoleGraphics
{
    public class Engine
    {
        private readonly char[,] buffer;
        private readonly int width;
        private readonly int height;

        public Engine(int consoleWidth, int consoleHeight)
        {
            Console.SetWindowSize(consoleWidth, consoleHeight);
            Console.SetBufferSize(consoleWidth, consoleHeight);
            width = consoleWidth;
            height = consoleHeight - 1;
            buffer = new char[width, height];
            Console.CursorVisible = false;
        }

        public void ClearBuffer()
        {
            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    buffer[x, y] = ' ';
        }

        public void AddCircleToBuffer(int h, int k, float radius)
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
                        AddCharacterToBuffer(x, y, '·');
                    }
                }
            }
        }

        public void AddLineToBuffer(int startX, int startY, int endX, int endY)
        {
            if (startX == endX && startY == endY)
            {
                AddPointToBuffer(startX, startY);
                return;
            }

            if (startX == endX)
            {
                AddVerticalLineToBuffer(startX, startY, endY);
                return;
            }

            if (endX == endY)
            {
                AddHorizontalLineToBuffer(startY, startX, endX);
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
                        AddCharacterToBuffer(x, y, charToUse);
                    }
                }
            }
        }

        public void AddPointToBuffer(int x, int y)
        {
            AddCharacterToBuffer(x, y, 'o');
        }

        public void AddVerticalLineToBuffer(int x, int startY, int endY)
        {
            if (startY > endY)
            {
                (startY, endY) = (endY, startY);
            }

            for (int y = startY; y <= endY; y++)
            {
                AddCharacterToBuffer(x, y, '|');
            }
        }

        public void AddHorizontalLineToBuffer(int y, int startX, int endX)
        {
            if (startX > endX)
            {
                (startX, endX) = (endX, startX);
            }

            for (int x = startX; x <= endX; x++)
            {
                AddCharacterToBuffer(y, x, '-');
            }
        }

        public void AddStringToBuffer(int x, int y, string str)
        {
            for (int index = 0; index < str.Length; index++)
            {
                AddCharacterToBuffer(x + index, y, str[index]);
            }
        }

        private void AddCharacterToBuffer(int x, int y, char c)
        {
            if (x >= 0 && x < width && y >= 0 && y < height)
                buffer[x, y] = c;
        }

        public void RenderBuffer()
        {
            Console.SetCursorPosition(0, 0);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(buffer[x, y]);
                }
                Console.WriteLine();
            }
        }
    }
}

