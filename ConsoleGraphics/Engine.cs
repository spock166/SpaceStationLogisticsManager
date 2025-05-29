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

        public void DrawStringToBuffer(int x, int y, string str)
        {
            for (int index = 0; index < str.Length; index++)
            {
                DrawToBuffer(x + index, y, str[index]);
            }
        }

        private void DrawToBuffer(int x, int y, char c)
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
