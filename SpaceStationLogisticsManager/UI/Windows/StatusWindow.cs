using SpaceStationLogisticsManager.GameLogic;
using Terminal.Gui;

namespace SpaceStationLogisticsManager.UI.Windows
{

    /// <summary>
    /// Represents a window that displays the current status of the game.
    /// </summary>
    public class StatusWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StatusWindow"/> class.
        /// </summary>
        /// <param name="title">The title of the window.</param>
        /// <param name="gameState">The game state to track and display.</param>
        public StatusWindow(string title, Engine gameEngine) : base(title)
        {
            // Subscribe to the OnTickCompleted event to refresh the status when the tick changes.
            gameEngine.OnTickCompleted += (args) => RefreshStatus(args.CurrentTick);

            // Initialize the status display.
            RefreshStatus(gameEngine.CurrentState.CurrentTick);
        }

        /// <summary>
        /// Refreshes the status display with the current tick value.
        /// </summary>
        /// <param name="currentTick">The current tick value to display.</param>
        private void RefreshStatus(int currentTick)
        {
            // Clear all existing content in the window.
            RemoveAll();

            // Add a label to display the current tick.
            Label tickLabel = new Label("Tick: " + currentTick)
            {
                X = Pos.Left(this),
                Y = 0
            };
            Add(tickLabel);
        }
    }
}