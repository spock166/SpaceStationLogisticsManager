using DockingModule;

namespace SpaceStationLogisticsManager
{
    public class Program
    {
        private static readonly Random rng = new Random();

        public static void Main(string[] args)
        {
            bool running = true;
            int currentTick = 0;
            NavigationMap map = new NavigationMap(3, 3);
            ConsoleGraphics graphics = new ConsoleGraphics(120, 40);

            while (running)
            {
                graphics.ClearBuffer();
                graphics.DrawStringToBuffer(0, 0, "Press Enter to advance time.");
                graphics.DrawStringToBuffer(120 - 10, 0, "Tick " + currentTick);
                graphics.DrawStringToBuffer(0, 1, "Hello World!");
                graphics.RenderBuffer();

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyPressed = Console.ReadKey(true);
                    if (keyPressed.Key == ConsoleKey.Enter)
                    {
                        currentTick++;
                    }
                }

            }
        }
    }
}