using DockingModule.NavigationNodes;
using ShipModule;

namespace DockingModule
{
    /// <summary>
    /// Represents the navigation map of the docking module.
    /// </summary>
    public class NavigationMap
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationMap"/> class.
        /// </summary>
        /// <param name="numRings">The number of rings in the navigation map.</param>
        /// <param name="numSegments">The number of segments in each ring.</param>
        /// <exception cref="ArgumentException">Thrown when <paramref name="numRings"/> is less than 1 or <paramref name="numSegments"/> is less than 3.</exception>
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
            ShipRoutes = new Dictionary<Ship, List<INavigationNode>>();

            RingCount = numRings;
            SegmentCount = numSegments;

            Nodes.Add(new DockingNode(0, 0));

            for (int ring = 1; ring <= numRings; ring++)
            {
                for (int segment = 0; segment < numSegments; segment++)
                {
                    Nodes.Add(new NavigationNode(ring, segment));
                }
            }
        }

        /// <summary>
        /// Gets the list of navigation nodes in the map.
        /// </summary>
        public List<INavigationNode> Nodes { get; }

        /// <summary>
        /// Gets the dictionary mapping ships to their current navigation nodes.
        /// </summary>
        public Dictionary<Ship, INavigationNode> Ships { get; }

        public Dictionary<Ship, List<INavigationNode>> ShipRoutes { get; }

        /// <summary>
        /// Gets the number of rings in the navigation map.
        /// </summary>
        public int RingCount { get; private set; }

        /// <summary>
        /// Gets the number of segments in each ring.
        /// </summary>
        public int SegmentCount { get; private set; }

        /// <summary>
        /// Retrieves a navigation node based on its ring and segment coordinates.
        /// </summary>
        /// <param name="ring">The ring coordinate of the node.</param>
        /// <param name="segment">The segment coordinate of the node.</param>
        /// <returns>The navigation node at the specified coordinates.</returns>
        public INavigationNode GetNode(int ring, int segment)
        {
            return Nodes[GetIndexFromCoordinate(ring, segment)];
        }

        /// <summary>
        /// Retrieves a navigation node based on its index.
        /// </summary>
        /// <param name="index">The index of the node.</param>
        /// <returns>The navigation node at the specified index.</returns>
        public INavigationNode GetNode(int index)
        {
            GetCoordinateFromIndex(index, out int ring, out int segment);
            return GetNode(ring, segment);
        }

        /// <summary>
        /// Converts ring and segment coordinates to a node index.
        /// </summary>
        /// <param name="ring">The ring coordinate.</param>
        /// <param name="segment">The segment coordinate.</param>
        /// <returns>The index of the node.</returns>
        public int GetIndexFromCoordinate(int ring, int segment)
        {
            if (ring == 0)
            {
                return 0;
            }

            segment = (SegmentCount + segment) % SegmentCount + 1;
            return SegmentCount * (ring - 1) + segment;
        }

        /// <summary>
        /// Converts a node index to ring and segment coordinates.
        /// </summary>
        /// <param name="index">The index of the node.</param>
        /// <param name="ring">The ring coordinate of the node.</param>
        /// <param name="segment">The segment coordinate of the node.</param>
        /// <exception cref="Exception">Thrown when the index is out of bounds.</exception>
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

        /// <summary>
        /// Retrieves valid adjacent navigation nodes based on the current node's coordinates and direction.
        /// </summary>
        /// <param name="ring">The ring coordinate of the current node.</param>
        /// <param name="segment">The segment coordinate of the current node.</param>
        /// <param name="direction">The direction of the ship.</param>
        /// <returns>A list of valid adjacent navigation nodes.</returns>
        public List<INavigationNode> GetValidNodes(int ring, int segment, ShipDirection direction)
        {
            List<INavigationNode> adjacentNodes = [GetNode(ring, segment)];

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

        /// <summary>
        /// Retrieves valid adjacent navigation nodes based on the current node's index and direction.
        /// </summary>
        /// <param name="index">The index of the current node.</param>
        /// <param name="direction">The direction of the ship.</param>
        /// <returns>A list of valid adjacent navigation nodes.</returns>
        public List<INavigationNode> GetValidNodes(int index, ShipDirection direction)
        {
            GetCoordinateFromIndex(index, out int ring, out int segment);

            return GetValidNodes(ring, segment, direction);
        }

        /// <summary>
        /// Determines if a navigation node is free (not occupied by a ship).
        /// </summary>
        /// <param name="node">The navigation node to check.</param>
        /// <returns>True if the node is free; otherwise, false.</returns>
        private bool IsNodeFree(INavigationNode node)
        {
            if (node is DockingNode)
            {
                return true; // Docking node is always free
            }

            if (node is NavigationNode navNode)
            {
                return !Ships.Values.Any(shipNode => shipNode.Ring == navNode.Ring && shipNode.Segment == navNode.Segment);
            }

            return false; // Unknown node type
        }

        /// <summary>
        /// Retrieves the ship located at the specified ring and segment.
        /// </summary>
        /// <param name="ring">The ring coordinate of the node.</param>
        /// <param name="segment">The segment coordinate of the node.</param>
        /// <returns>The ship at the specified location, or null if no ship is present.</returns>
        public Ship? GetShipAt(int ring, int segment)
        {
            INavigationNode node = GetNode(ring, segment);
            if (node is DockingNode)
            {
                return null; // Docking node does not have a ship
            }

            if (node is NavigationNode navNode)
            {
                return Ships.FirstOrDefault(ship => ship.Value.Ring == navNode.Ring && ship.Value.Segment == navNode.Segment).Key;
            }

            return null;
        }

        /// <summary>
        /// Attempts to retrieve a ship by its registry.
        /// </summary>
        /// <param name="ship">The output parameter to store the retrieved ship.</param>
        /// <param name="shipRegistry">The registry of the ship to retrieve.</param>
        /// <returns>True if the ship is found; otherwise, false.</returns>
        public bool TryGetShip(out Ship? ship, string shipRegistry)
        {
            ship = Ships.Keys.FirstOrDefault(s => s.Registry.ToString() == shipRegistry);
            if (ship != null)
            {
                return true;
            }
            ship = null;
            return false;
        }

        /// <summary>
        /// Retrieves the route of a specified ship.
        /// </summary>
        /// <param name="ship">The ship whose route is to be retrieved.</param>
        /// <returns>A list of navigation nodes representing the ship's route.</returns>
        public List<INavigationNode> GetShipRoute(Ship? ship)
        {
            if (ship == null)
            {
                return new List<INavigationNode>(); // Return an empty list if no ship is provided
            }

            if (ShipRoutes.TryGetValue(ship, out List<INavigationNode>? route))
            {
                return route;
            }

            return new List<INavigationNode>(); // Return an empty list if no route exists for the ship
        }

        /// <summary>
        /// Retrieves a list of ship registries currently in the navigation map.
        /// </summary>
        /// <returns>A list of ship registries as strings.</returns>
        public List<string> GetShipRegistries()
        {
            return Ships.Keys.Select(ship => ship.Registry.ToString()).ToList();
        }

        /// <summary>
        /// Retrieves a list of open inbound nodes for ships.
        /// </summary>
        /// <returns>A list of navigation nodes that are open for inbound ships.</returns>
        private List<INavigationNode> GetOpenInboundNode()
        {
            List<INavigationNode> openNodes = new List<INavigationNode>();

            for (int segment = 0; segment < SegmentCount; segment++)
            {
                INavigationNode node = GetNode(RingCount - 1, segment);
                if (IsNodeFree(node))
                {
                    openNodes.Add(node);
                }
            }

            return openNodes;
        }

        /// <summary>
        /// Attempts to add an inbound ship to the navigation map.
        /// </summary>
        /// <returns>True if the ship was successfully added; otherwise, false.</returns>
        public bool TryAddInboundShip()
        {
            List<INavigationNode> openNodes = GetOpenInboundNode();

            if (openNodes.Count == 0)
            {
                return false; // No open nodes available for inbound ships
            }

            INavigationNode selectedNode = openNodes[0]; // Select the first available node for simplicity
            Ship newShip = new Ship(ShipDirection.Inbound);
            Ships.Add(newShip, selectedNode);
            ShipRoutes.Add(newShip, new List<INavigationNode> { selectedNode });
            return true;
        }
    }
}
