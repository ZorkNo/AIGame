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
           
            IAiType blueAiType = new SimpleAiType();
            blueAiType.SetRandomGenerator(rnd);
            IAiType redAiType = new SimpleAiType();
            redAiType.SetRandomGenerator(rnd);
            Game game = new Game(blueAiType,redAiType,40,60,rnd); ;
            for (int i =0;i<100000;i++)
            {
                Console.Clear();
                game.Render();
                game.NextTurn();
                
                Console.ReadKey();

            }
        }
    }
}
