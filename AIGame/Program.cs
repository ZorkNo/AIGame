using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.AI;
using AIGame.League;
namespace AIGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("G for Single game");
            Console.WriteLine("L for 10.000 match ups");
            ConsoleKeyInfo key =Console.ReadKey();

            if(key.Key == ConsoleKey.G)
            {
                SingleGame(); 
            }
            else
            {
                MatchUp();
            }
            
        }

        private static void SingleGame()
        {
            Random rnd = new Random(Environment.TickCount);

            IAiType blueAiType = new RandomAiType();
            blueAiType.SetRandomGenerator(rnd);
            IAiType redAiType = new SimpleAiType();
            redAiType.SetRandomGenerator(rnd);
            Game game = new Game(blueAiType, redAiType, 10, 10, rnd);
            ;
            for (int i = 0; i < 100000; i++)
            {
                Console.Clear();
                game.Render();
                game.NextTurn();

                Console.ReadKey();
            }
        }

        private static void MatchUp()
        {
            League.League league = new League.League();

            league.RunSingleMatchUp();
        }
    }
}
