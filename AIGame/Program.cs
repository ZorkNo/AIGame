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
            for(int i =0;i<100;i++)
            { 
                Game game = new Game(new SimpleAiType(), new SimpleAiType()); ;
                game.Render();
                Console.ReadKey();

            }
        }
    }
}
