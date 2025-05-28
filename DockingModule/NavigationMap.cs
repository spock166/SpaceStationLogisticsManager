namespace DockingModule
{
    public class NavigationMap
    {
        public NavigationMap(int numRings, int numSegments)
        {
            Nodes = new List<NavigationNode>(numSegments * numRings);
            this.numRings = numRings;
            this.numSegments = numSegments;

            for (int ring = 0; ring < numRings; ring++)
            {
                for (int segment = 0; segment < numSegments; segment++)
                {
                    Nodes.Add(new NavigationNode(ring, segment));
                }
            }
        }
        public List<NavigationNode> Nodes { get; }

        private readonly int numRings;
        private readonly int numSegments;

        public NavigationNode GetNode(int ring, int segment)
        {
            return Nodes[numRings * segment + ring];
        }

        public NavigationNode GetNode(int index)
        {
            int segment = index % numRings;
            int ring = index / numRings;
            return GetNode(ring, segment);
        }
    }
}
