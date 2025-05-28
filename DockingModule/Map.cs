namespace DockingModule
{
    internal class Map
    {
        public Map(int numRings, int numSegments)
        {
            Nodes = new List<NavigationNode>(numSegments * numRings);
            this.numRings = numRings;
            this.numSegments = numSegments;

            for (int ring = 0; ring < numRings; ring++)
            {
                for (int segment = 0; segment < numSegments; segment++)
                {
                    Nodes[numSegments * ring + segment] = new NavigationNode(ring, segment);
                }
            }
        }
        public List<NavigationNode> Nodes { get; }

        private readonly int numRings;
        private readonly int numSegments;

        public NavigationNode GetNode(int ring, int segment)
        {
            return Nodes[numSegments * ring + segment];
        }

        public NavigationNode GetNode(int index)
        {
            int segment = index % numSegments;
            int ring = index / numSegments;
            return GetNode(ring, segment);
        }
    }
}
