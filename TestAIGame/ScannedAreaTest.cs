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

        [Test]
        public void ScanNorthTest()
        {
            var map = GetMap();

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
        public void ScanNorth2Test()
        {
            var map = GetMap();


            map.Terrain[0, 0].Type = TerrainType.Land;

            MockUnit.Setup(u => u.Facing).Returns(Direction.North);
            MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2, 0));
            
            scannedArea.Initilize(MockUnit.Object, map);

            Assert.AreEqual(TerrainType.Land, scannedArea.Terrain[0, 0].Type,
                        "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1} render:{2}", 0, 0, scannedArea.RenderArea());
               
        }
        [Test]
        public void ScanSourth2Test()
        {
            var map = GetMap();


            map.Terrain[0, 0].Type = TerrainType.Land;

            MockUnit.Setup(u => u.Facing).Returns(Direction.South);
            MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2, 0));

            scannedArea.Initilize(MockUnit.Object, map);

            Assert.AreEqual(TerrainType.Land, scannedArea.Terrain[4,0].Type,
                        "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1} render:{2}", 4, 0, scannedArea.RenderArea());

        }

        [Test]
        public void ScanSouthTest()
        {
            var map = GetMap();

            MockUnit.Setup(u => u.Facing).Returns(Direction.South);
            MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2, 0));
            
            scannedArea.Initilize(MockUnit.Object, map);

            for (int x = 0; x < scannedArea.XSize; x++)
            {
                for (int y = 0; y < scannedArea.YSize; y++)
                {
                    if (y > 0)
                    {
                        Assert.AreEqual( TerrainType.Edge, scannedArea.Terrain[x, y].Type,
                           "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1} render:{2}", x, y, scannedArea.RenderArea());
                    }
                    else
                    {
                        Assert.AreEqual(scannedArea.Terrain[x, y].Type, TerrainType.Sea,
                        "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1}", x, y);
                    }
                    
                }
            }
        }
        [Test]
        public void ScanEastTest()
        {
            var map = GetMap();

            MockUnit.Setup(u => u.Facing).Returns(Direction.East);
            MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2, 0));
            
            scannedArea.Initilize(MockUnit.Object, map);

            for (int x = 0; x < scannedArea.XSize; x++)
            {
                for (int y = 0; y < scannedArea.YSize; y++)
                {
                    if (x < 2)
                    {
                        Assert.AreEqual(TerrainType.Edge, scannedArea.Terrain[x, y].Type,
                           "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1} render:{2}", x, y, scannedArea.RenderArea());
                    }
                    else
                    {
                        Assert.AreEqual(scannedArea.Terrain[x, y].Type, TerrainType.Sea,
                        "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1}", x, y);
                    }

                }
            }
        }
        //[Test]
        //public void ScanWestTest()
        //{
        //    var map = GetMap();

        //    MockUnit.Setup(u => u.Facing).Returns(Direction.West);
        //    MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2, 0));
            
        //    scannedArea.Initilize(MockUnit.Object, map);

        //    for (int x = 0; x < scannedArea.XSize; x++)
        //    {
        //        for (int y = 0; y < scannedArea.YSize; y++)
        //        {
        //            if (x >= 3)
        //            {
        //                Assert.AreEqual(TerrainType.Edge, scannedArea.Terrain[x, y].Type,
        //                   "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1}", x, y);
        //            }
        //            else
        //            {
        //                Assert.AreEqual(scannedArea.Terrain[x, y].Type, TerrainType.Sea,
        //                "scannedArea.Terrain[x,y].Equals(new Terrain(TerrainType.Sea)) x:{0} y:{1} render:{2}", x, y, scannedArea.RenderArea());
        //            }

        //        }
        //    }
        //}
        private static IMap GetMap()
        {
            int xSize = 5;
            int ySize = 3;
            Terrain[,] Terrain = new Terrain[xSize, ySize];

            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    Terrain[x, y] = new Terrain(TerrainType.Sea);
                }
            }
            IMap map = new Map(xSize, ySize, new Random(), new List<IUnit>());
            map.Terrain = Terrain;
            return map;
        }

        
        [Test]
        [TestCase(Direction.West, 2, 1, 3, 0)]
        [TestCase(Direction.East, 2, 1, 1, 0)]
        [TestCase(Direction.North, 2, 1, 2, 1)]
        [TestCase(Direction.South , 2, 1, 2, -1)]
        public void ConvertMapCoordinatesTest(Direction facing, int x, int y, int resultx,int resulty)
        {
            Tuple<int, int> unitCoor = new Tuple<int, int>(2, 0);
            Tuple<int, int> coor = scannedArea.ConvertMapCoordinates(facing, unitCoor, new Tuple<int, int>((x - 2), y));
            Assert.AreEqual( resultx,coor.Item1, coor.Item1.ToString());
            Assert.AreEqual(resulty, coor.Item2, coor.Item2.ToString());
        }
    }
}
