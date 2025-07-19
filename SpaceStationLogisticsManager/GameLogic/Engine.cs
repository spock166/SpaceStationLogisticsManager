using DockingModule;
using DockingModule.NavigationNodes;
using ShipModule;

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
        private GameState CurrentState { get; set; }

        /// <summary>
        /// Event triggered when the tick is incremented.
        /// </summary>
        public event Action<TickCompletedEventArgs> OnTickCompleted;

        /// <summary>
        /// Event triggered when a ship is selected.
        /// </summary>
        public event Action<Ship>? OnShipSelected;

        /// <summary>
        /// Event triggered when a ship is deselected.
        /// </summary>
        public event Action? OnShipDeselected;

        /// <summary>
        /// Event triggered when the selected ship or its route changes.
        /// </summary>
        public event Action<Ship?, List<INavigationNode>?> OnShipSelectionChanged;

        private List<INavigationNode> selectedRouteCache;

        /// <summary>
        /// Initializes a new instance of the <see cref="Engine"/> class.
        /// </summary>
        public Engine()
        {
            CurrentState = new GameState { Map = new NavigationMap(3, 4) };
            OnTickCompleted = delegate { }; // Initialize to an empty delegate to avoid null checks
            OnShipSelectionChanged = delegate { }; // Initialize to an empty delegate to avoid null checks
            selectedRouteCache = new List<INavigationNode>();
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

        /// <summary>
        /// Selects a ship based on its registry and triggers relevant events.
        /// </summary>
        /// <param name="shipRegistry">The registry of the ship to select.</param>
        public void SelectShip(string shipRegistry)
        {
            CurrentState.Map.TryGetShip(out Ship? ship, shipRegistry);
            Ship? previousShip = CurrentState.SelectedShip;
            CurrentState.SelectedShip = ship;
            if (previousShip != ship)
            {
                selectedRouteCache = CurrentState.Map.GetShipRoute(ship);
            }

            if (ship != null)
            {
                OnShipSelected?.Invoke(ship);
            }
            else
            {
                OnShipDeselected?.Invoke();
            }

            OnShipSelectionChanged?.Invoke(ship, selectedRouteCache);
        }

        /// <summary>
        /// Retrieves a list of ship registries currently in the game state.
        /// </summary>
        /// <returns>A list of ship registries as strings.</returns>
        public List<string> GetShipRegistries()
        {
            return CurrentState.Map.GetShipRegistries();
        }

        /// <summary>
        /// Retrieves the navigation map from the current game state.
        /// </summary>
        /// <returns>The current navigation map.</returns>
        public NavigationMap GetMap()
        {
            return CurrentState.Map;
        }

        /// <summary>
        /// Retrieves the current tick value from the game state.
        /// </summary>
        /// <returns>The current tick value.</returns>
        public int GetCurrentTick()
        {
            return CurrentState.CurrentTick;
        }

        /// <summary>
        /// Retrieves the currently selected ship from the game state.
        /// </summary>
        /// <returns>The currently selected ship, or null if no ship is selected.</returns>
        public Ship? GetCurrentShip()
        {
            return CurrentState.SelectedShip;
        }

        /// <summary>
        /// Retrieves the route of the currently selected ship.
        /// </summary>
        /// <returns>A list of navigation nodes representing the route of the selected ship.</returns>
        public List<INavigationNode> GetCurrentShipRoute()
        {
            return CurrentState.Map.GetShipRoute(CurrentState.SelectedShip);
        }

        /// <summary>
        /// Gets the chance of an inbound ship spawning.
        /// </summary>
        private double GetInboundSpawnChance()
        {
            // TODO: Implement logic to determine inbound ship spawn chance
            return 0.1; // Placeholder value
        }
    }
}