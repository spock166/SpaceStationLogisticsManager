using DockingModule;
using Terminal.Gui;

namespace SpaceStationLogisticsManager.UI.Windows
{
    /// <summary>
    /// Represents a window for displaying the docking map.
    /// </summary>
    public class MapWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapWindow"/> class.
        /// </summary>
        /// <param name="title">The title of the window.</param>
        public MapWindow(string title) : base(title)
        {
            LayoutStarted += (LayoutEventArgs layoutEventArgs) =>
            {
                Application.Driver.Move(layoutEventArgs.OldBounds.Width / 2, layoutEventArgs.OldBounds.Height / 2);
                Application.Driver.AddRune('●'); // Draw a dot at the center
            };
        }

        /// <summary>
        /// Refreshes the docking map within the window.
        /// </summary>
        /// <param name="topSize">The size of the top-level window.</param>
        /// <param name="map">The navigation map to display.</param>
        public void RefreshMap(Size topSize, NavigationMap map)
        {
            RemoveAll();
            int width = topSize.Width / 2; // mapWindow takes up half the width
            int height = topSize.Height - 4;

            int centerX = width / 2;
            int centerY = height / 2 - 2; // Adjust for status bar height
            int numRings = map.RingCount;
            int numSegments = map.SegmentCount;

            int radius = height * 2 / 5;

            for (int ring = 0; ring < numRings; ring++)
            {
                Graphics.AddCircleToWindow(this, centerX, centerY, (float)(radius * (ring + 1)) / numRings);
            }

            float deltaRadii = 2 * (float)Math.PI / numSegments;

            for (float angle = 0; angle < 2 * (float)Math.PI; angle += deltaRadii)
            {
                int x = (int)(radius * Math.Cos(angle)) + centerX;
                int y = (int)(radius * Math.Sin(angle)) + centerY;
                Graphics.AddLineToWindow(this, centerX, centerY, x, y);
                for (int ring = 0; ring < numRings; ring++)
                {
                    x = (int)(radius * (ring + 1) * Math.Cos(angle) / numRings) + centerX;
                    y = (int)(radius * (ring + 1) * Math.Sin(angle) / numRings) + centerY;
                    Graphics.AddPointToWindow(this, x, y);
                }
            }
            Graphics.AddPointToWindow(this, centerX, centerY);
        }
    }
}