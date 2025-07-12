using DockingModule;

namespace SpaceStationLogisticsManager.GameLogic
{
    /// <summary>
    /// The main engine of the game that manages the game state.
    /// </summary>
    public class Engine
    {
        private static readonly Random rng = new Random();

        /// <summary>
        /// The current state of the game.
        /// </summary>
        public GameState CurrentState { get; private set; }

        /// <summary>
        /// Event triggered when the tick is incremented.
        /// </summary>
        public event Action<TickCompletedEventArgs> OnTickCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            CurrentState = new GameState { Map = new NavigationMap(3, 4) };
            OnTickCompleted = delegate { }; // Initialize to an empty delegate to avoid null checks
        }

        /// <summary>
        /// Increments the current tick and triggers the OnTickCompleted event.
        /// </summary>
        public void NextTick()
        {
            // TODO: Move all ships on the map

            if (rng.NextDouble() < GetInboundSpawnChance())
            {
                CurrentState.Map.TryAddInboundShip();
            }

            // TODO: Add logic for outbound ships

            CurrentState.CurrentTick++;
            OnTickCompleted?.Invoke(new TickCompletedEventArgs(CurrentState.CurrentTick));
        }

        /// <summary>
        /// Gets the chance of an inbound ship spawning.
        /// </summary>
        private double GetInboundSpawnChance()
        {
            // TODO: Implement logic to determine inbound ship spawn chance
            return 0.1; // Placeholder value
        }

        /// <summary>
        /// Provides data for the <see cref="OnTickCompleted"/> event.
        /// </summary>
        public class TickCompletedEventArgs : EventArgs
        {
            /// <summary>
            /// Gets the current tick value after the tick has been incremented.
            /// </summary>
            public int CurrentTick { get; }

            /// <summary>
            /// Initializes a new instance of the <see cref="TickCompletedEventArgs"/> class.
            /// </summary>
            /// <param name="currentTick">The current tick value.</param>
            public TickCompletedEventArgs(int currentTick)
            {
                CurrentTick = currentTick;
            }
        }
    }
}