using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.AI;
namespace AIGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(new SimpleAiType(), new SimpleAiType()); ;
            for (int i =0;i<100;i++)
            {
                game.NextTurn();
                game.Render();
                Console.ReadKey();

            }
        }
    }
}
