using DockingModule;
using DockingModule.NavigationNodes;
using ShipModule;

namespace DockingModuleTests
{
    [TestClass]
    public sealed class MapTests
    {
        private const int NUM_RINGS = 3;
        private const int NUM_SEGMENTS = 4;
        private readonly NavigationMap testMap = new NavigationMap(NUM_RINGS, NUM_SEGMENTS);

        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(1, 1, 0)]
        [DataRow(NUM_SEGMENTS + 1, 2, 0)]
        [DataRow(NUM_RINGS * NUM_SEGMENTS, NUM_RINGS, NUM_SEGMENTS - 1)]
        public void GetCoordinate_FromIndex_GetsCorrectNode(int index, int expectedRing, int expectedSegment)
        {
            testMap.GetCoordinateFromIndex(index, out int ring, out int segment);
            Assert.AreEqual(expectedRing, ring, "Ring Mismatch");
            Assert.AreEqual(expectedSegment, segment, "Segment Mismatch");
        }


        [TestMethod]
        [DataRow(0, 0, 0)]
        [DataRow(1, 0, 1)]
        [DataRow(2, 0, NUM_SEGMENTS + 1)]
        [DataRow(NUM_RINGS, NUM_SEGMENTS - 1, NUM_RINGS * NUM_SEGMENTS)]
        public void GetIndex_FromCoordinate_GetsCorrectIndex(int ring, int segment, int expectedIndex)
        {
            int index = testMap.GetIndexFromCoordinate(ring, segment);
            Assert.AreEqual(expectedIndex, index);
        }

        [TestMethod]
        public void ExpectNode_DockingNode_IsOrigin()
        {
            INavigationNode node = testMap.GetNode(0);
            Assert.IsInstanceOfType(node, typeof(DockingNode));
        }

        [TestMethod]
        [DataRow(0, 0, ShipDirection.Outbound, NUM_SEGMENTS + 1)]
        [DataRow(0, 0, ShipDirection.Inbound, 1)]
        [DataRow(NUM_RINGS, 0, ShipDirection.Inbound, 4)]
        [DataRow(NUM_RINGS, 0, ShipDirection.Outbound, 3)]
        [DataRow(NUM_RINGS - 1, 0, ShipDirection.Inbound, 4)]
        [DataRow(NUM_RINGS - 1, 0, ShipDirection.Outbound, 4)]
        public void GetValidNodes_FromNode_GetsCorrectCount(int ring, int segment, ShipDirection direction, int expectedCount)
        {
            Assert.AreEqual(expectedCount, testMap.GetValidNodes(ring, segment, direction).Count);
        }
    }
}
