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
        FireTorpedo sut;
        [SetUp]
        public void Setup()
        {
            MockMap = new Mock<IMap>();
            MockUnit = new Mock<IUnit>();
            MockUnits = new Mock<List<IUnit>>();

            sut = new FireTorpedo(new Tuple<int, int>(0,0));
        }
        [Test]
        [TestCase(Direction.North, -2, 2,4,0)]
        [TestCase(Direction.East, 2, 2, 4, 0)]
        public void Fire(Direction facing, int x, int y,int targetX,int targetY)
        {
            MockUnit.Setup(u => u.Facing).Returns(facing);
            MockUnit.Setup(u => u.Coordinates).Returns(new Tuple<int, int>(2, 2));
            
            List<IUnit> units = new List<IUnit>();
            Unit unitOnMap = new Unit("a", Side.Blue, new DoNothingAI());
            unitOnMap.Coordinates = new Tuple<int, int>(targetX, targetY);
            units.Add(unitOnMap);
            MockMap.Setup(m => m.Units).Returns(units);

            sut.RelativeCoordinates = new Tuple<int, int>(x,y);
            sut.Execute(MockUnit.Object,MockMap.Object);

            Assert.AreEqual(66,unitOnMap.Health);
        }
    }
}