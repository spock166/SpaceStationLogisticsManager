using DockingModule;

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
        public void GetNode_FromIndex_GetsCorrectNode(int index, int expectedRing, int expectedSegment)
        {
            INavigationNode node = testMap.GetNode(index);
            Assert.AreEqual(expectedRing, node.Ring, "Ring Mismatch");
            Assert.AreEqual(expectedSegment, node.Segment, "Segment Mismatch");
        }

        [TestMethod]
        public void ExpectNode_DockingNode_IsOrigin()
        {
            INavigationNode node = testMap.GetNode(0);
            Assert.IsInstanceOfType(node, typeof(DockingNode));
        }
    }
}
