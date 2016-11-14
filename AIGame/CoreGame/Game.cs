using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.CoreGame
{
    public class Game
    {
        public int turn=0;
        public int maxTurn = 100;
        public Map map;
        public List<Unit> units;
        public bool gameEnded = false;

        public Game(iAiType blue, iAiType red,int xSize,int ySize,Random rnd)
        {
            map = new Map(xSize, ySize, rnd);

            units = new List<Unit>();

            units.Add(new Unit("A", Side.blue, blue.GetAi(), map.GetValidStartPosition(units)));
            units.Add(new Unit("X", Side.red, red.GetAi(), map.GetValidStartPosition(units)));
            units.Add(new Unit("B", Side.blue, blue.GetAi(), map.GetValidStartPosition(units)));
            units.Add(new Unit("Y", Side.red, red.GetAi(), map.GetValidStartPosition(units)));
        }
        public void PlayUntilEnd()
        {
            for (int i=0;i >maxTurn;i++)
            {
                NextTurn();
                if (IsGameEnded())
                    break;

            }
        }

        private bool IsGameEnded()
        {

            //No more units

            //Only one type of units

            //Max turns

            return false;
        }

        public void NextTurn()
        {
            foreach(Unit unit in units)
            {
                unit.UpdateSensor(map);
                IOrder order = unit.GetOrder();
                if (order.IsValid(unit, map))
                { 
                    order.Execute(unit, map);
                }
            }
            turn++;   
        }
        public void Render()
        {
            RenderBoard();
        }
        private void RenderBoard()
        {
            
            for (int x = 0; x <= map.xSize; x++)
            {
                string line = "";    
                for (int y = 0; y <= map.ySize; y++)
                {
                    line += RenderCoordinate(x, y);
                }
                Console.WriteLine(line);
            }
            Console.WriteLine("Turn:" + turn);
        }
        private string RenderCoordinate(int x, int y)
        {
            foreach(Unit unit in units)
            {
                if (unit.coordinates.Equals( new Tuple<int, int>(x, y)))
                    return unit.name;
            }
            return map.terrain[x, y].Render();
        }


    }
}
