using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.League;
using NUnit.Framework;

namespace TestAIGame
{
    [TestFixture]
    public class EloRatingTest
    {
        [Test]
        public void test()
        {
            double blueElo = 2400;
            double redElo = 2000;

            double newBlueElo = EloRatingCalc.GetRating(Result.Win, blueElo, redElo);

            Assert.AreEqual(2402.909090909091d, newBlueElo);
        }

        [Test]
        public void test2()
        {
            double blueElo = 2400;
            double redElo = 2000;

            double newRedElo = EloRatingCalc.GetRating(Result.Lost, redElo,blueElo);

            Assert.AreEqual(1997.090909090909d, newRedElo);
        }
        [Test]
        public void TieTest()
        {
            double blueElo = 2400;
            double redElo = 2000;

            double newRedElo = EloRatingCalc.GetRating(Result.Tie, redElo, blueElo);
            double newBlueElo = EloRatingCalc.GetRating(Result.Tie, blueElo, redElo);

            Assert.AreEqual(2013.090909090909d, newRedElo);
            Assert.AreEqual(2386.909090909091d, newBlueElo);
        }
        [Test]
        public void Tie2Test()
        {
            double blueElo = 2000;
            double redElo = 2000;

            double newRedElo = EloRatingCalc.GetRating(Result.Tie, redElo, blueElo);
            double newBlueElo = EloRatingCalc.GetRating(Result.Tie, blueElo, redElo);

            Assert.AreEqual(2000, newRedElo);
            Assert.AreEqual(2000, newBlueElo);
        }
        //[Test]
        //public void Tie3Test()
        //{
        //    double blueElo = 2000;
        //    double redElo = 2000;

        //    double newRedElo = EloRatingCalc.GetRating(Result.Win, redElo, blueElo);
        //    double newBlueElo = EloRatingCalc.GetRating(Result.Lost, blueElo, redElo);

        //    Assert.AreEqual(2000, newRedElo);
        //    Assert.AreEqual(2000, newBlueElo);
        //}

    }
}
