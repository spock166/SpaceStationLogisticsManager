using DockingModule.NavigationNodes;
using ShipModule;
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
            gameEngine.OnTickCompleted += (args) => RefreshStatus(args.CurrentTick, gameEngine.GetCurrentShip(), gameEngine.GetCurrentShipRoute());

            // Initialize the status display.
            RefreshStatus(gameEngine.GetCurrentTick(), gameEngine.GetCurrentShip(), gameEngine.GetCurrentShipRoute());

            gameEngine.OnShipSelectionChanged += (ship, shipRoute) =>
            {
                // Optionally, you can update the status display when the ship selection changes.
                // For now, we just refresh the current tick.
                RefreshStatus(gameEngine.GetCurrentTick(), ship, shipRoute);
            };
        }

        /// <summary>
        /// Refreshes the status display with the current tick value.
        /// </summary>
        /// <param name="currentTick">The current tick value to display.</param>
        private void RefreshStatus(int currentTick, Ship? currentShip, List<INavigationNode>? shipRoute)
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

            Label shipLabel = new Label("Current Ship: " + (currentShip?.ToString() ?? "None"))
            {
                X = Pos.Left(this),
                Y = 1
            };

            Add(shipLabel);

            if (currentShip != null)
            {
                Label shipRouteLabel = new Label("Route: " + string.Join("\n", shipRoute?.Select(node => node.ToString()) ?? []))
                {
                    X = Pos.Left(this),
                    Y = 2
                };
                Add(shipRouteLabel);
            }
        }
    }
}