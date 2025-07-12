using DockingModule;

namespace SpaceStationLogisticsManager.GameLogic
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
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        public GameState()
        {
            CurrentTick = 0;
            Map = new NavigationMap(3, 4); // Default map dimensions      
        }
    }
}