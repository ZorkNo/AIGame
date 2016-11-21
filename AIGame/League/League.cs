using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame;

namespace AIGame.League
{
    public class League
    {
        private int gamesPerMatchUp=10000;
        private int redPoints = 0;
        private int bluePoints = 0;

        public void RunSingleMatchUp()
        {
            Random rnd = new Random(Environment.TickCount);

            IAiType blueAiType = new RandomAiType();
            blueAiType.SetRandomGenerator(rnd);
            IAiType redAiType = new SimpleAiType();
            redAiType.SetRandomGenerator(rnd);

            DateTime starTime = DateTime.Now;
            for (int i = 0; i < gamesPerMatchUp; i++)
            {
                Game game = new Game(blueAiType, redAiType, 50, 50, rnd); ;

                game.PlayUntilEnd();

                UpdateScore(game);
            }
            TimeSpan CalculationTime = DateTime.Now.Subtract(starTime);
            Console.WriteLine("Match ups: {0} Calculation time milliseconds:{1}", gamesPerMatchUp, CalculationTime.TotalMilliseconds);
            Console.WriteLine("Score results red:{0} blue:{1}", redPoints, bluePoints);
            Console.ReadKey();
        }

        private void UpdateScore(Game game)
        {
            switch (game.GameResult)
            {
                case GameResult.RedWin:
                    redPoints += 2;
                    break;
                case GameResult.BlueWin:
                    bluePoints += 2;
                    break;
                case GameResult.Tie:
                    redPoints += 1;
                    bluePoints += 1;
                    break;
                default:
                    throw new Exception("Unknown result");
            }
        }
    }
}
