﻿using ShipModule;

namespace DockingModule.NavigationNodes
{
    public class DockingNode : INavigationNode
    {
        public DockingNode(int ring, int segment)
        {
            Ring = ring;
            Segment = segment;
            DockedShips = new List<Ship>();
        }

        public override string ToString()
        {
            return $"Ring: {Ring}, Segment: {Segment}";
        }

        public int Ring { get; }
        public int Segment { get; }
        public List<Ship> DockedShips { get; }
    }
}
