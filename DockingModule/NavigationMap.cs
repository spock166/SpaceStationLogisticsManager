using DockingModule.NavigationNodes;
using ShipModule;

namespace DockingModule
{
    public class NavigationMap
    {
        public NavigationMap(int numRings, int numSegments)
        {
            if (numRings < 1)
            {
                throw new ArgumentException("numRings is expected to be at least 1.");
            }

            if (numSegments < 3)
            {
                throw new ArgumentException("numSegments is expected to be at least 3.");
            }

            Nodes = new List<INavigationNode>(numSegments * numRings + 1);
            Ships = new Dictionary<Ship, INavigationNode>();

            this.RingCount = numRings;
            this.SegmentCount = numSegments;

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

        public int RingCount { get; private set; }
        public int SegmentCount { get; private set; }

        public INavigationNode GetNode(int ring, int segment)
        {
            return Nodes[GetIndexFromCoordinate(ring, segment)];
        }

        public INavigationNode GetNode(int index)
        {
            GetCoordinateFromIndex(index, out int ring, out int segment);
            return GetNode(ring, segment);
        }

        public int GetIndexFromCoordinate(int ring, int segment)
        {
            if (ring == 0)
            {
                return 0;
            }

            segment = (SegmentCount + segment) % SegmentCount + 1;
            return SegmentCount * (ring - 1) + segment;
        }

        public void GetCoordinateFromIndex(int index, out int ring, out int segment)
        {
            if (index > Nodes.Count - 1 || index < 0)
            {
                throw new Exception("Attempting to get map coordinate for index " + index + " but Nodes only has " + Nodes.Count + " elements.");
            }

            if (index == 0)
            {
                ring = 0;
                segment = 0;
                return;
            }

            ring = (index - 1) / SegmentCount + 1;
            segment = (index - 1) % SegmentCount;
        }

        public List<INavigationNode> GetValidNodes(int ring, int segment, ShipDirection direction)
        {
            List<INavigationNode> adjacentNodes = new List<INavigationNode>();
            adjacentNodes.Add(GetNode(ring, segment));

            if (ring == 0 && segment == 0) // Docking node
            {
                if (direction == ShipDirection.Inbound)
                {
                    return adjacentNodes;
                }

                for (int s = 0; s < SegmentCount; s++)
                {
                    adjacentNodes.Add(GetNode(1, s));
                }

                return adjacentNodes;
            }

            adjacentNodes.Add(GetNode(ring, segment + 1));
            adjacentNodes.Add(GetNode(ring, segment - 1));

            if (direction == ShipDirection.Outbound)
            {
                if (ring == RingCount)
                {
                    return adjacentNodes;
                }

                adjacentNodes.Add(GetNode(ring + 1, segment));
                return adjacentNodes;
            }

            adjacentNodes.Add(GetNode(ring - 1, segment));

            return adjacentNodes;

        }

        public List<INavigationNode> GetValidNodes(int index, ShipDirection direction)
        {
            GetCoordinateFromIndex(index, out int ring, out int segment);

            return GetValidNodes(ring, segment, direction);
        }
    }
}
