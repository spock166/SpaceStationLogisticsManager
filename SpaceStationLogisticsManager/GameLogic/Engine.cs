using DockingModule;

namespace SpaceStationLogisticsManager.GameLogic
{
    public class Engine
    {
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
            CurrentState.CurrentTick++;
            OnTickCompleted?.Invoke(new TickCompletedEventArgs(CurrentState.CurrentTick));
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