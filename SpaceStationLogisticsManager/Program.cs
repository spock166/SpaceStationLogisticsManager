using ShipModule;

namespace SpaceStationLogisticsManager
{
    public class Program
    {
        private static readonly Random rng = new Random();

        public static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                Ship testShip = rng.NextDouble() < 0.8 ? new Ship() : new Ship(rng.Next(11));
                Console.WriteLine("Registry: " + testShip.Registry);
                Console.WriteLine("Displayed Checksum: " + testShip.Registry.DisplayedChecksum);
                Console.WriteLine("Actual Checksum: " + testShip.Registry.GenerateChecksum());
                Console.WriteLine("----");
            }

            Console.ReadKey();
        }
    }
}