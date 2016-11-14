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
            Random rnd= new Random(Environment.TickCount);
            RandomAiType blueAiType = new RandomAiType();
            blueAiType.Rnd = rnd;
            RandomAiType redAiType = new RandomAiType();
            redAiType.Rnd = rnd;

            Game game = new Game(blueAiType,redAiType,40,60,rnd); ;
            for (int i =0;i<100;i++)
            {
                Console.Clear();
                game.Render();
                game.NextTurn();
                
                Console.ReadKey();

            }
        }
    }
}
