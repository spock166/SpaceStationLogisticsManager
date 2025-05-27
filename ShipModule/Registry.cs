namespace ShipModule
{
    public readonly struct Registry
    {
        public readonly string FirstHalf;
        public readonly string SecondHalf;
        public readonly int DisplayedChecksum;
        private const int PART_LENGTH = 3;

        public Registry(string firstHalf, string secondHalf) : this(firstHalf, secondHalf, GenerateChecksum(firstHalf, secondHalf)) { }

        public Registry(string firstHalf, string seecondHalf, int displayedChecksum)
        {
            if (firstHalf.Length != PART_LENGTH || seecondHalf.Length != PART_LENGTH)
            {
                throw new ArgumentException("Registry had total length " + (firstHalf.Length + seecondHalf.Length) + " expected length " + (2 * PART_LENGTH) + ".");
            }

            FirstHalf = firstHalf;
            SecondHalf = seecondHalf;
            DisplayedChecksum = displayedChecksum;
        }

        #region Public Methods
        public int GenerateChecksum()
        {
            return GenerateChecksum(FirstHalf, SecondHalf);
        }

        public override string ToString()
        {
            return FirstHalf + "-" + SecondHalf;
        }
        #endregion

        #region Private Methods
        private static int GenerateChecksum(string firstHalf, string secondHalf)
        {
            int sum = 0;
            foreach (char item in firstHalf)
            {
                sum += item;
            }

            foreach (char item in secondHalf)
            {
                sum += item;
            }

            return sum %= 10;
        }
        #endregion
    }
}
