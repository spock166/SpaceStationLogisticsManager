namespace ShipModule
{
    public static class ShipUtils
    {
        private static readonly Random rng = new Random();

        public static string DirectionString(ShipDirection direction)
        {
            switch (direction)
            {
                case ShipDirection.Inbound:
                    return "Inbound";
                case ShipDirection.Outbound:
                    return "Outbound";
                default:
                    return "--"; // Not expected
            }
        }

        public static Ship RandomShip(double probFalseRegistry = 0.1)
        {
            ShipDirection direction = rng.Next() % 2 == 0 ? ShipDirection.Inbound : ShipDirection.Outbound;
            return RandomShip(direction, probFalseRegistry);
        }

        public static Ship RandomShip(ShipDirection direction, double probFalseRegistry = 0.1)
        {
            return rng.NextDouble() < probFalseRegistry ? new Ship(direction, rng.Next(11)) : new Ship(direction);
        }
    }
}
