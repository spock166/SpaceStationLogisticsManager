namespace DockingModule
{
    public class NavigationNode
    {
        public NavigationNode(int radius, int segment)
        {
            Radius = radius;
            Segment = segment;
        }

        public int Radius { get; }
        public int Segment { get; }
    }
}
