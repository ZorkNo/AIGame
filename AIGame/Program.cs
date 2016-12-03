using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.AI;
using AIGame.Interfaces;
using AIGame.League;
namespace AIGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("G for Single game");
            Console.WriteLine("L for League match ups");
            ConsoleKeyInfo key =Console.ReadKey();

            if(key.Key == ConsoleKey.L)
            {
                LeagueMatchUp();
            }
            else
            {
                SingleGame();
            }
            
        }

        private static void SingleGame()
        {
            Random rnd = new Random(Environment.TickCount);
            
            var game = Game.Create<RandomAI,SimpleAi>(GameMode.HiddenInfo1ShipSmall, rnd);

            for (int i = 0; i < 100000; i++)
            {
                Console.Clear();
                game.Render();
                game.NextTurn();

                Console.ReadKey();
            }
        }

        private static void LeagueMatchUp()
        {
            League.League league = new League.League();

            league.RunSingleMatchUp();
        }
    }
}
