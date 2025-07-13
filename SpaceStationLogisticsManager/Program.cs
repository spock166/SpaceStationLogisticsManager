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
            CreateUI(engine, top);
            Application.Run();
            Application.Shutdown();
        }

        /// <summary>
        /// Creates the user interface for the application.
        /// </summary>
        /// <param name="engine">The game engine instance.</param>
        /// <param name="top">The top-level application window.</param>
        private static void CreateUI(Engine engine, Toplevel top)
        {
            // Create menu bar
            MenuBar topMenu = CreateMenuBar();
            top.Add(topMenu);
            TabView tabPane = CreateTabPane(engine, top);
            Window menuPane = CreateMenuPane(tabPane);

            top.Add(tabPane, menuPane);

            // Create status bar
            StatusBar statusBar = CreateStatusBar(engine);
            top.Add(statusBar);

            top.AddKeyBinding(Key.CtrlMask | Key.Q, Command.QuitToplevel);

        }

        /// <summary>
        /// Creates the operations pane for the application.
        /// </summary>
        /// <param name="tabPane">The tab view to align the operations pane with.</param>
        /// <returns>A <see cref="Window"/> instance representing the operations pane.</returns>
        private static Window CreateMenuPane(TabView tabPane)
        {

            // Create operations pane
            Window menuPane = new Window("Operations") { X = Pos.Right(tabPane), Y = 0, Width = Dim.Percent(50), Height = Dim.Fill() };
            ListView topLevelMenu = CreateTopLevelMenu();
            menuPane.Add(topLevelMenu);
            return menuPane;
        }

        /// <summary>
        /// Creates the top-level menu for the operations pane.
        /// </summary>
        /// <returns>A <see cref="ListView"/> instance representing the top-level menu.</returns>
        private static ListView CreateTopLevelMenu()
        {
            ListView topLevelMenu = new ListView(new string[]
            {
                "Select Ship",
                "Quit Game"
            })
            {
                X = 0,
                Y = 0,
                Width = Dim.Fill(),
                Height = Dim.Fill(),
            };
            topLevelMenu.OpenSelectedItem += (args) =>
            {
                switch (args.Value)
                {
                    case "Select Ship":
                        MessageBox.Query("Select Ship", "Ship selection is not yet implemented.", "OK");
                        break;
                    case "Quit Game":
                        QuitQuery();
                        break;
                }
            };
            return topLevelMenu;
        }

        /// <summary>
        /// Displays a confirmation dialog for quitting the application.
        /// </summary>
        private static void QuitQuery()
        {
            int result = MessageBox.Query("Quit Game", "Are you sure you want to quit?", "Yes", "No");
            if (result == 0)
            {
                Application.RequestStop();
            }
        }

        /// <summary>
        /// Creates the tab view for the application.
        /// </summary>
        /// <param name="engine">The game engine instance.</param>
        /// <param name="top">The top-level application window.</param>
        /// <returns>A <see cref="TabView"/> instance representing the tab view.</returns>
        private static TabView CreateTabPane(Engine engine, Toplevel top)
        {

            // Create tab view
            TabView tabPane = new TabView() { X = 0, Y = 1, Width = Dim.Percent(50), Height = Dim.Fill() - 1 };

            // Create docking map pane
            MapWindow mapPane = new MapWindow("Docking Map") { X = 0, Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };

            TabView.Tab mapTab = new TabView.Tab("Docking Map", mapPane);
            AddMapEventHandlers(engine, top, tabPane, mapPane, mapTab);
            tabPane.AddTab(mapTab, true);

            // Create status pane
            StatusWindow statusPane = new StatusWindow("Status", engine) { X = Pos.Right(mapPane), Y = 0, Width = Dim.Fill(), Height = Dim.Fill() };
            TabView.Tab statusTab = new TabView.Tab("Status", statusPane);
            tabPane.AddTab(statusTab, false);


            mapPane.RefreshMap(top.Frame.Size, engine.CurrentState.Map);
            return tabPane;
        }

        /// <summary>
        /// Adds event handlers for the docking map tab.
        /// </summary>
        /// <param name="engine">The game engine instance.</param>
        /// <param name="top">The top-level application window.</param>
        /// <param name="tabPane">The tab view containing the docking map tab.</param>
        /// <param name="mapPane">The docking map window.</param>
        /// <param name="mapTab">The docking map tab.</param>
        private static void AddMapEventHandlers(Engine engine, Toplevel top, TabView tabPane, MapWindow mapPane, TabView.Tab mapTab)
        {
            top.Resized += (Size newSize) =>
            {
                if (tabPane.SelectedTab == mapTab)
                {
                    mapPane.RefreshMap(newSize, engine.CurrentState.Map);
                }
            };

            tabPane.SelectedTabChanged += (sender, e) =>
            {
                if (e.NewTab == mapTab)
                {
                    mapPane.RefreshMap(top.Frame.Size, engine.CurrentState.Map);
                }
            };

            engine.OnTickCompleted += (args) =>
            {
                if (tabPane.SelectedTab == mapTab)
                {
                    mapPane.RefreshMap(top.Frame.Size, engine.CurrentState.Map);
                }
            };
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
        /// <returns>A <see cref="MenuBar"/> instance representing the menu bar.</returns>
        private static MenuBar CreateMenuBar()
        {
            return new MenuBar(
            [
                new MenuBarItem("_File",
                [
                    new MenuItem("_Quit", "Quit the application", QuitQuery)
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
        /// <param name="gameEngine">The game engine instance.</param>
        /// <returns>A <see cref="StatusBar"/> instance representing the status bar.</returns>
        private static StatusBar CreateStatusBar(Engine gameEngine)
        {
            return new StatusBar(
            [
                new StatusItem(Key.CtrlMask | Key.Q, "~^Q~ Quit", () => Application.RequestStop()),
                new StatusItem(Key.F1, "~F1~ Help", () => MessageBox.Query("Help", "Show help dialog", "OK")),
                new StatusItem(Key.a, "~A~dvance Tick", gameEngine.NextTick),
            ]);
        }
    }
}