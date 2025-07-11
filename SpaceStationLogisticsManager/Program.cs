using SpaceStationLogisticsManager.GameLogic;
using SpaceStationLogisticsManager.UI.Windows;
using Terminal.Gui;

namespace SpaceStationLogisticsManager
{
    /// <summary>
    /// Entry point for the Space Station Logistics Manager application.
    /// </summary>
    public class Program
    {
        private static readonly Random rng = new Random();

        /// <summary>
        /// Main method to initialize and run the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Engine engine = new Engine();

            Application.Init();
            Toplevel top = Application.Top;

            // Set color scheme for the application
            ConfigureColors();

            // Create menu bar
            MenuBar topMenu = CreateMenuBar();
            top.Add(topMenu);

            // Create tab view
            TabView tabView = new TabView() { X = 0, Y = 1, Width = Dim.Percent(50), Height = Dim.Fill() - 1 };

            // Create docking map pane
            MapWindow mapPane = new MapWindow("Docking Map") { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            // Create status pane
            StatusWindow statusPane = new StatusWindow("Status", engine) { X = Pos.Right(mapPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            TabView.Tab mapTab = new TabView.Tab("Docking Map", mapPane);
            TabView.Tab statusTab = new TabView.Tab("Status", statusPane);
            tabView.AddTab(mapTab, true);
            tabView.AddTab(statusTab, false);

            // Create operations pane
            Window menuPane = new Window("Operations") { X = Pos.Right(tabView), Y = 0, Width = Dim.Percent(50), Height = Dim.Fill() };

            // Create status bar
            StatusBar statusBar = CreateStatusBar(engine, mapPane, top);
            top.Add(tabView, menuPane);
            top.Add(statusBar);

            AddEventHandlers(top, tabView, mapTab, mapPane, statusPane, engine);

            mapPane.RefreshMap(top.Frame.Size, engine.CurrentState.Map);
            Application.Run();
            Application.Shutdown();
        }

        /// <summary>
        /// Configures the color scheme for the application.
        /// </summary>
        private static void ConfigureColors()
        {
            Colors.Base.Normal = Application.Driver.MakeAttribute(Color.White, Color.Black);
            Colors.Base.Focus = Application.Driver.MakeAttribute(Color.Black, Color.Gray);
            Colors.Base.HotNormal = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Black);
            Colors.Base.HotFocus = Application.Driver.MakeAttribute(Color.BrightYellow, Color.Gray);
        }

        /// <summary>
        /// Creates the menu bar for the application.
        /// </summary>
        /// <returns>A <see cref="MenuBar"/> instance.</returns>
        private static MenuBar CreateMenuBar()
        {
            return new MenuBar(
            [
                new MenuBarItem("_File",
                [
                    new MenuItem("_Quit", "Quit the application", () => Application.RequestStop())
                ]),
                new MenuBarItem("_Help",
                [
                    new MenuItem("_About", "Show about dialog", () => MessageBox.Query("About", "Space Station Logistics Manager", "OK"))
                ])
            ]);
        }

        /// <summary>
        /// Creates the status bar for the application.
        /// </summary>
        /// <param name="gameEngine">The game engine to track.</param>
        /// <param name="mapPane">The map window to refresh.</param>
        /// <param name="top">The top-level application window.</param>
        /// <returns>A <see cref="StatusBar"/> instance.</returns>
        private static StatusBar CreateStatusBar(Engine gameEngine, MapWindow mapPane, Toplevel top)
        {
            return new StatusBar(
            [
                new StatusItem(Key.CtrlMask | Key.Q, "~^Q~ Quit", () => Application.RequestStop()),
                new StatusItem(Key.F1, "~F1~ Help", () => MessageBox.Query("Help", "Show help dialog", "OK")),
                new StatusItem(Key.a, "~A~dvance Tick", () =>
                {
                    gameEngine.NextTick();
                    mapPane.RefreshMap(top.Frame.Size, gameEngine.CurrentState.Map);
                }),
            ]);
        }

        /// <summary>
        /// Adds event handlers for resizing and tab changes.
        /// </summary>
        /// <param name="top">The top-level application window.</param>
        /// <param name="tabView">The tab view to monitor.</param>
        /// <param name="mapTab">The map tab to refresh.</param>
        /// <param name="mapPane">The map window to refresh.</param>
        /// <param name="statusPane">The status window to update.</param>
        /// <param name="gameEngine">The game engine to track.</param>
        private static void AddEventHandlers(Toplevel top, TabView tabView, TabView.Tab mapTab, MapWindow mapPane, Window statusPane, Engine gameEngine)
        {
            top.Resized += (Size newSize) =>
            {
                if (tabView.SelectedTab == mapTab)
                {
                    mapPane.RefreshMap(newSize, gameEngine.CurrentState.Map);
                }
            };

            tabView.SelectedTabChanged += (sender, e) =>
            {
                if (e.NewTab == mapTab)
                {
                    mapPane.RefreshMap(top.Frame.Size, gameEngine.CurrentState.Map);
                }
            };

            gameEngine.OnTickCompleted += (args) =>
            {
                if (tabView.SelectedTab == mapTab)
                {
                    mapPane.RefreshMap(top.Frame.Size, gameEngine.CurrentState.Map);
                }
            };

            top.AddKeyBinding(Key.CtrlMask | Key.Q, Command.QuitToplevel);
        }
    }
}