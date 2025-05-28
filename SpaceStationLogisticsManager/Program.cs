using DockingModule;
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
                Ship testShip = rng.NextDouble() < 0.8 ? new Ship(ShipDirection.Inbound) : new Ship(ShipDirection.Outbound, rng.Next(11));
                Console.WriteLine("Registry: " + testShip.Registry);
                Console.WriteLine("Displayed Checksum: " + testShip.Registry.DisplayedChecksum);
                Console.WriteLine("Actual Checksum: " + testShip.Registry.GenerateChecksum());
                Console.WriteLine("Ship Direction: " + testShip.Direction);
                Console.WriteLine("----");
            }

            Console.ReadKey();

            NavigationMap map = new NavigationMap(3, 3);
            Console.WriteLine("Map Generated");
            Console.WriteLine(map.GetNode(3));
            Console.ReadKey();
        }
    }
}