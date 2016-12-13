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
            while(true)
            {
                Console.Clear();
                Console.WriteLine("G for Single game");
                Console.WriteLine("L for League match ups");
                Console.WriteLine("E for Ecosystem");
                ConsoleKeyInfo key =Console.ReadKey();

                if(key.Key == ConsoleKey.L)
                    LeagueMatchUp();

                if (key.Key == ConsoleKey.G)
                    SingleGame();

                if (key.Key == ConsoleKey.E)
                    Ecosystem();
                
            }

        }

        private static void SingleGame()
        {
            Random rnd = new Random(Environment.TickCount);
            var game = Game.Create( AiType.Create<SimplePlusAI>(), AiType.Create<CooroperateAI>(),
                GameMode.HiddenInfo2ShipLarge, rnd);

            for (int i = 0; i < 100000; i++)
            {
                Console.Clear();
                game.Render();
                game.NextTurn();


                Console.ReadKey();

                if (game.GameResult != GameResult.GameNotEnded)
                    break;
            }
            Console.WriteLine("Game result:{0}", game.GameResult);
            Console.WriteLine("Press N to continue");
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey();

                if (key.Key == ConsoleKey.N)
                    break;
            }
        }

        private static void LeagueMatchUp()
        {
            Console.Clear();
            League.League league = new League.League();

            league.Tournament();
            Console.ReadKey();
        }
        private static void Ecosystem()
        {
            Console.Clear();
            Ecosystem ecosystem = new Ecosystem();

            ecosystem.RunEco();
            Console.ReadKey();
        }
    }
}
