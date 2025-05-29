namespace ShipModule
{
    public class Ship
    {

        public Ship(ShipDirection direction)
        {
            Direction = direction;
            Registry = GenerateRegistry();
        }

        public Ship(ShipDirection direction, int displayedChecksum) : this(direction)
        {
            Registry = GenerateRegistry(displayedChecksum);
        }

        private static readonly Random rng = new Random();

        public ShipDirection Direction { get; }
        public Registry Registry { get; private set; }

        #region Public Methods
        #endregion

        #region Private Methods
        private static Registry GenerateRegistry(int displayedChecksum)
        {
            string firstHalf, secondHalf;
            RandomRegistry(out firstHalf, out secondHalf);

            return new Registry(firstHalf, secondHalf, displayedChecksum);
        }

        private static Registry GenerateRegistry()
        {
            string firstHalf, secondHalf;
            RandomRegistry(out firstHalf, out secondHalf);

            return new Registry(firstHalf, secondHalf);
        }

        private static void RandomRegistry(out string firstHalf, out string secondHalf)
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            firstHalf = "";
            for (int i = 0; i < 3; i++)
            {
                firstHalf += chars[rng.Next(chars.Length)];
            }

            chars = "0123456789";
            secondHalf = "";
            for (int i = 0; i < 3; i++)
            {
                secondHalf += chars[rng.Next(chars.Length)];
            }
        }
        #endregion
    }
}
