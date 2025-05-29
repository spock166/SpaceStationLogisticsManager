namespace DockingModule.NavigationNodes
{
    public class NavigationNode : INavigationNode
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
