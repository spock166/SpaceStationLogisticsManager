using ShipModule;

namespace DockingModule
{
    public class NavigationMap
    {
        public NavigationMap(int numRings, int numSegments)
        {
            Nodes = new List<INavigationNode>(numSegments * numRings + 1);
            Ships = new Dictionary<Ship, INavigationNode>();

            this.numRings = numRings;
            this.numSegments = numSegments;

            Nodes.Add(new DockingNode(0, 0));

            for (int ring = 1; ring <= numRings; ring++)
            {
                for (int segment = 0; segment < numSegments; segment++)
                {
                    Nodes.Add(new NavigationNode(ring, segment));
                }
            }
        }
        public List<INavigationNode> Nodes { get; }
        public Dictionary<Ship, INavigationNode> Ships { get; }

        private readonly int numRings;
        private readonly int numSegments;

        public INavigationNode GetNode(int ring, int segment)
        {
            return Nodes[numSegments * ring + segment];
        }

        public INavigationNode GetNode(int index)
        {
            int ring = index / numSegments;
            int segment = index % numSegments;
            return GetNode(ring, segment);
        }
    }
}
