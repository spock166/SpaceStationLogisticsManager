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

            //Hard-code map for now
            int numRings = 3;
            int numSegments = 4;
            NavigationMap map = new NavigationMap(numRings, numSegments);
            Engine engine = new Engine(CONSOLE_WIDTH, CONSOLE_HEIGHT);
            Console.Title = "Space Station Logistics Manager";
            DrawBasicScreen(currentTick, map, engine);
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

                    DrawBasicScreen(currentTick, map, engine);
                }

            }
        }

        private static void DrawBasicScreen(int currentTick, NavigationMap map, Engine engine)
        {
            engine.ClearBuffer();
            engine.AddStringToBuffer(0, 0, "Press Enter to advance time.");
            engine.AddStringToBuffer(CONSOLE_WIDTH - 10, 0, "Tick " + currentTick);
            engine.AddStringToBuffer(0, 1, "Hello World!");
            DrawNavigationMap(engine, map);
            engine.RenderBuffer();
        }

        private static void DrawNavigationMap(Engine engine, NavigationMap map)
        {
            int centerX = CONSOLE_WIDTH / 2;
            int centerY = CONSOLE_HEIGHT / 2;
            int numRings = map.RingCount;
            int numSegments = map.SegmentCount;

            int radius = (CONSOLE_HEIGHT * 2) / 5;

            for (int ring = 0; ring < numRings; ring++)
            {
                engine.AddCircleToBuffer(centerX, centerY, (float)(radius * (ring + 1)) / numRings);
            }

            float deltaDegree = 360.0f / numSegments;

            for (float degree = 0; degree < 360; degree += deltaDegree)
            {
                int x = (int)(radius * Math.Cos(degree * Math.PI / 180)) + centerX;
                int y = (int)(radius * Math.Sin(degree * Math.PI / 180)) + centerY;
                engine.AddLineToBuffer(centerX, centerY, x, y);
                for (int ring = 0; ring < numRings; ring++)
                {
                    x = (int)(radius * (ring + 1) * Math.Cos(degree * Math.PI / 180) / numRings) + centerX;
                    y = (int)(radius * (ring + 1) * Math.Sin(degree * Math.PI / 180) / numRings) + centerY;
                    engine.AddPointToBuffer(x, y);
                }
            }
            engine.AddPointToBuffer(centerX, centerY);
        }
    }
}