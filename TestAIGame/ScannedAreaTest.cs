using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using AIGame.CoreGame;
using AIGame.Interfaces;

namespace TestAIGame
{
    [TestFixture]
    public class ScannedAreaTest
    {
        private Mock<IMap> MockMap;
        private Mock<IUnit> MockUnit;
        //ScannedArea scannedArea;
        [SetUp]
        public void Setup()
        {
            MockMap = new Mock<IMap>() ;
            MockUnit = new Mock<IUnit>();

            //scannedArea = new ScannedArea();
        }

        //[Test, Ignore("not working")]
        //public void ScanNorthTest()
        //{
        //    var map = TestHelper.GetMap();

        //    MockUnit.Setup(u => u.Facing).Returns(Direction.North);
        //    MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2,0));

        //    scannedArea = new ScannedArea();
        //    scannedArea.Initilize(MockUnit.Object, map);

        //    for (int x = 0; x < scannedArea.XSize ; x++)
        //    {
        //        for (int y = 0; y < scannedArea.YSize; y++)
        //        {
        //            Assert.AreEqual( TerrainType.Sea, scannedArea.Terrain[x, y].Type,
        //                "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1} render:{2}",x,y, scannedArea.RenderArea());
        //        }
        //    }
        //}
        
        

        

    }
}
