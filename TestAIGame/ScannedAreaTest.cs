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
        ScannedArea scannedArea;
        [SetUp]
        public void Setup()
        {
            MockMap = new Mock<IMap>() ;
            MockUnit = new Mock<IUnit>();

            scannedArea = new ScannedArea();
        }

        [Test, Ignore("not working")]
        public void ScanNorthTest()
        {
            var map = TestHelper.GetMap();

            MockUnit.Setup(u => u.Facing).Returns(Direction.North);
            MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2,0));

            scannedArea = new ScannedArea();
            scannedArea.Initilize(MockUnit.Object, map);

            for (int x = 0; x < scannedArea.XSize ; x++)
            {
                for (int y = 0; y < scannedArea.YSize; y++)
                {
                    Assert.AreEqual( TerrainType.Sea, scannedArea.Terrain[x, y].Type,
                        "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1} render:{2}",x,y, scannedArea.RenderArea());
                }
            }
        }
        
        

        
        [Test]
        [Ignore("")]
        [TestCase(Direction.North, 2, 1, 3, 0)]
        [TestCase(Direction.South, 2, 1, 1, 0)]
        [TestCase(Direction.West , 2, 1, 2, 1)]
        [TestCase(Direction.East , 2, 1, 2, -1)]
        public void ConvertMapCoordinatesTest(Direction facing, int x, int y, int resultx,int resulty)
        {
            Tuple<int, int> unitCoor = new Tuple<int, int>(2, 0);
            Tuple<int, int> coor = scannedArea.ConvertMapCoordinates(facing, unitCoor, new Tuple<int, int>((x - 2), y));
            Assert.AreEqual( resultx,coor.Item1, coor.Item1.ToString());
            Assert.AreEqual(resulty, coor.Item2, coor.Item2.ToString());
        }
    }
}
