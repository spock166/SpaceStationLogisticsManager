using DockingModule;

namespace DockingModuleTests
{
    [TestClass]
    public sealed class MapTests
    {
        private readonly NavigationMap testMap = new NavigationMap(3, 3);

        [TestMethod]
        public void GetNode_FromIndex_GetsCorrectNode()
        {
            NavigationNode node = testMap.GetNode(0);
            Assert.AreEqual(0, node.Segment);
            Assert.AreEqual(0, node.Ring);

            node = testMap.GetNode(1);
            Assert.AreEqual(0, node.Segment);
            Assert.AreEqual(1, node.Ring);

            node = testMap.GetNode(3);
            Assert.AreEqual(1, node.Segment);
            Assert.AreEqual(0, node.Ring);
        }
    }
}
