using DockingModule;

namespace SpaceStationLogisticsManager
{
    /// <summary>
    /// Represents the state of the game, including the current tick and navigation map.
    /// </summary>
    public class GameState
    {
        /// <summary>
        /// Gets or sets the current tick of the game.
        /// </summary>
        public int CurrentTick { get; set; }

        /// <summary>
        /// Gets or sets the navigation map of the game.
        /// </summary>
        public required NavigationMap Map { get; set; }

        /// <summary>
        /// Event triggered when the tick is incremented.
        /// </summary>
        public event Action<TickCompletedEventArgs> OnTickCompleted;

        public GameState()
        {
            CurrentTick = 0;
            Map = new NavigationMap(3, 4); // Default map dimensions
            OnTickCompleted = delegate { }; // Initialize to an empty delegate to avoid null checks
        }

        /// <summary>
        /// Increments the current tick and triggers the OnTickCompleted event.
        /// </summary>
        public void NextTick()
        {
            CurrentTick++;
            OnTickCompleted?.Invoke(new TickCompletedEventArgs(CurrentTick));
        }
    }

    /// <summary>
    /// Provides data for the <see cref="GameState.OnTickCompleted"/> event.
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