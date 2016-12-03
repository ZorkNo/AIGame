using System;
using System.Collections.Generic;
using AIGame.AI;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;
using Castle.Components.DictionaryAdapter;
using Moq;
using NUnit.Framework;

namespace TestAIGame
{
    [TestFixture]
    public class FireTorpedoTest
    {
        private Mock<IMap> MockMap;
        private Mock<IUnit> MockUnit;
        private Mock<List<IUnit>> MockUnits;
        private Mock<Random> MockRandom;
        FireTorpedo sut;
        [SetUp]
        public void Setup()
        {
            MockMap = new Mock<IMap>();
            MockUnit = new Mock<IUnit>();
            MockUnits = new Mock<List<IUnit>>();
            MockRandom = new Mock<Random>();

            sut = new FireTorpedo(new Tuple<int, int>(0,0));
        }
        [Test]
        [TestCase(Direction.North, -1, 2, 1, 4)]
        [TestCase(Direction.East, -1, 2, 0, 1)]
        [TestCase(Direction.South, -1, 2, 3, 0)]
        [TestCase(Direction.West, -1, 2, 4, 3)]
        public void Fire(Direction facing, int x, int y,int targetX,int targetY)
        {
            MockUnit.Setup(u => u.Facing).Returns(facing);
            MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2, 2));
            
            List<IUnit> units = new List<IUnit>();
            Unit unitOnMap = new Unit("a", Side.Blue, new DoNothingAI(MockRandom.Object));
            unitOnMap.Coordinates = new Tuple<int, int>(targetX, targetY);
            units.Add(unitOnMap);
            MockMap.Setup(m => m.Units).Returns(units);

            sut.RelativeCoordinates = new Tuple<int, int>(x,y);
            sut.Execute(MockUnit.Object,MockMap.Object);

            Assert.AreEqual(66,unitOnMap.Health);
        }
          [Test,TestCaseSource("IsValidCases")]
        public void IsValidTest(int x, int y,bool valid)
        {
            sut.RelativeCoordinates = new Tuple<int, int>(x, y);
            bool isValid = sut.IsValid(MockUnit.Object , MockMap.Object);

            Assert.AreEqual(valid, isValid);
        }
        public static object[] IsValidCases()
        {
            List<object> objects = new List<object>();
            for (int x=-10; x<10;x++)
            {
                for (int y = -10; y<10; y++)
                {
                    objects.Add( new object[] { x, y,(x<=-2 && x >=2 && y>=0 && y<=2) });
                }
            }
            return objects.ToArray();

        }
    }
}