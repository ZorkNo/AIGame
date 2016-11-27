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
        private MatchUp _matchUp = new MatchUp();
        private bool parallelGames = true;

        public void RunSingleMatchUp()
        {
            IAiType blueAiType = new RandomAiType();
            IAiType redAiType = new SimpleAiType();

            DateTime starTime = DateTime.Now;
            Console.Clear();
            Console.WriteLine("Running");
            RunAllGames(blueAiType, redAiType);

            Console.WriteLine("");
            TimeSpan CalculationTime = DateTime.Now.Subtract(starTime);
            Console.WriteLine("Match ups: {0} Calculation time milliseconds:{1}", gamesPerMatchUp, CalculationTime.TotalMilliseconds);
            Console.WriteLine("Score results {0}(blue):{1}:{2} {3}(red):{4}:{5}",
                blueAiType.Name, _matchUp.blueWins, _matchUp.blueTies,
                redAiType.Name, _matchUp.redWins, _matchUp.redTies);
            Console.ReadKey();
        }

        private void RunAllGames(IAiType blueAiType, IAiType redAiType)
        {
            if (!parallelGames)
            {
                for (int i = 0; i < gamesPerMatchUp; i++)
                {
                    PlayGame(blueAiType, redAiType, false, i);
                }
            }
            else
            {
                Parallel.For(0, gamesPerMatchUp, i => { PlayGame(blueAiType, redAiType, true, i); });
            }
        
        }

        private void PlayGame(IAiType blueAiType, IAiType redAiType,bool withLock, int gameInt)
        {
            long longRnd = Environment.TickCount + gameInt;
            Random rnd = new Random((int)longRnd);

            blueAiType.SetRandomGenerator(rnd);
            redAiType.SetRandomGenerator(rnd);

            Game game = new Game(blueAiType, redAiType, GameMode.HiddenInfo1ShipLarge, rnd);
            game.PlayUntilEnd();

            if(withLock)
            { 
                lock (_matchUp)
                {
                    _matchUp.AddGame(game);
                }
            }
            else
            {
                _matchUp.AddGame(game);
            }
            if (_matchUp.gamesPlayed % 250 == 0)
                Console.Write(".");
        }
    }
}
