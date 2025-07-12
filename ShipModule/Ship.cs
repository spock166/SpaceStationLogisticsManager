namespace ShipModule
{
    /// <summary>
    /// The ship data model.
    /// </summary>
    public class Ship
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ship"/> class with the specified direction.
        /// </summary>
        /// <param name="direction">The direction of the ship.</param>
        public Ship(ShipDirection direction)
        {
            Direction = direction;
            Registry = GenerateRegistry();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ship"/> class with the specified direction and displayed checksum.
        /// </summary>
        /// <param name="direction">The direction of the ship.</param>
        /// <param name="displayedChecksum">The checksum to display for the ship's registry.</param>
        public Ship(ShipDirection direction, int displayedChecksum) : this(direction)
        {
            Registry = GenerateRegistry(displayedChecksum);
        }

        private static readonly Random rng = new Random();

        /// <summary>
        /// Gets the direction of the ship.
        /// </summary>
        public ShipDirection Direction { get; }

        /// <summary>
        /// Gets the registry information of the ship.
        /// </summary>
        public Registry Registry { get; private set; }

        #region Public Methods
        /// <summary>
        /// Returns a string representation of the ship.
        /// </summary>
        /// <returns>A string containing the direction and registry of the ship.</returns>
        public override string ToString()
        {
            return $"Ship: {Direction}, Registry: {Registry}";
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Generates a registry for the ship using the specified checksum.
        /// </summary>
        /// <param name="displayedChecksum">The checksum to display for the registry.</param>
        /// <returns>A new <see cref="Registry"/> instance.</returns>
        private static Registry GenerateRegistry(int displayedChecksum)
        {
            string firstHalf, secondHalf;
            RandomRegistry(out firstHalf, out secondHalf);

            return new Registry(firstHalf, secondHalf, displayedChecksum);
        }

        /// <summary>
        /// Generates a registry for the ship.
        /// </summary>
        /// <returns>A new <see cref="Registry"/> instance.</returns>
        private static Registry GenerateRegistry()
        {
            string firstHalf, secondHalf;
            RandomRegistry(out firstHalf, out secondHalf);

            return new Registry(firstHalf, secondHalf);
        }

        /// <summary>
        /// Generates random registry components for the ship.
        /// </summary>
        /// <param name="firstHalf">The first half of the registry.</param>
        /// <param name="secondHalf">The second half of the registry.</param>
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
