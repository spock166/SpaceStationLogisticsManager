using ConsoleGraphics;
using DockingModule;

namespace SpaceStationLogisticsManager
{
    public class Program
    {
        private static readonly Random rng = new Random();
        private const int CONSOLE_WIDTH = 120;
        private const int CONSOLE_HEIGHT = 40;

        public static void Main(string[] args)
        {
            bool running = true;
            int currentTick = 0;
            NavigationMap map = new NavigationMap(3, 3);
            Engine graphics = new Engine(CONSOLE_WIDTH, CONSOLE_HEIGHT);
            DrawBasicScreen(currentTick, graphics);
            while (running)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                    switch (keyPressed.Key)
                    {
                        case ConsoleKey.Enter:
                            currentTick++;
                            break;
                        case ConsoleKey.Escape:
                            running = false;
                            break;
                        default:
                            break;
                    }

                    DrawBasicScreen(currentTick, graphics);
                }

            }
        }

        private static void DrawBasicScreen(int currentTick, Engine engine)
        {
            engine.ClearBuffer();
            engine.DrawStringToBuffer(0, 0, "Press Enter to advance time.");
            engine.DrawStringToBuffer(CONSOLE_WIDTH - 10, 0, "Tick " + currentTick);
            engine.DrawStringToBuffer(0, 1, "Hello World!");
            engine.RenderBuffer();
        }
    }
}