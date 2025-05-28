namespace DockingModule
{
    public class NavigationNode
    {
        public NavigationNode(int ring, int segment)
        {
            Ring = ring;
            Segment = segment;
        }

        public int Ring { get; }
        public int Segment { get; }
    }
}
