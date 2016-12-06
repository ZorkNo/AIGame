using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using NUnit.Framework;

namespace TestAIGame
{
    [TestFixture]
    public class HelperTest
    {
        [Test]
        public void GetDirectionNorthTest()
        {
            DirectionPrecise dp = Helper.GetDirection(new Tuple<int, int>(0, 0), new Tuple<int, int>(0, 1));

            Assert.AreEqual(DirectionPrecise.North,dp);
        }
        [Test]
        public void GetDirectionWestTest()
        {
            DirectionPrecise dp = Helper.GetDirection(new Tuple<int, int>(0, 0), new Tuple<int, int>(-1, 0));

            Assert.AreEqual(DirectionPrecise.West, dp);
        }
        [Test]
        public void GetDirectionEastTest()
        {
            DirectionPrecise dp = Helper.GetDirection(new Tuple<int, int>(0, 0), new Tuple<int, int>(1, 0));

            Assert.AreEqual(DirectionPrecise.East, dp);
        }
        [Test]
        public void GetDirectionSouthTest()
        {
            DirectionPrecise dp = Helper.GetDirection(new Tuple<int, int>(0, 0), new Tuple<int, int>(0, -1));

            Assert.AreEqual(DirectionPrecise.South, dp);
        }

        [Test]
        public void GetDirectionNorthAngleTest()
        {
            Double angle = Helper.GetAngle(new Tuple<int, int>(0, 0), new Tuple<int, int>(0, 1));

            Assert.AreEqual(0, angle);
        }
        [Test]
        public void GetDirectionWestAngleTest()
        {
            Double angle = Helper.GetAngle(new Tuple<int, int>(0, 0), new Tuple<int, int>(-1, 0));

            Assert.AreEqual(270, angle);
        }
        [Test]
        public void GetDirectionEastAngleTest()
        {
            Double angle = Helper.GetAngle(new Tuple<int, int>(0, 0), new Tuple<int, int>(1, 0));

            Assert.AreEqual(90, angle);
        }
        [Test]
        public void GetDirectionSouthAngleTest()
        {
            Double angle = Helper.GetAngle(new Tuple<int, int>(0, 0), new Tuple<int, int>(0, -1));

            Assert.AreEqual(180, angle);
        }
    }
}
