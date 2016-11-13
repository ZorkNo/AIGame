using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.AI;
using AIGame.CoreGame;

namespace AIGame.CoreGame
{
    public class Game
    {
        public int turn=0;
        public int maxTurn = 100;
        public Map map;
        public List<Unit> units;
        //public bool gameEnded = false;

        public Game(iAiType blue, iAiType red)
        {
            map = new Map(20,20);

            units = new List<Unit>();

            units.Add(new Unit("A", side.blue, blue.GetAi(), map.GetValidStartPosition(units)));
            units.Add(new Unit("X", side.red, red.GetAi(), map.GetValidStartPosition(units)));
            units.Add(new Unit("B", side.blue, blue.GetAi(), map.GetValidStartPosition(units)));
            units.Add(new Unit("Y", side.red, red.GetAi(), map.GetValidStartPosition(units)));
        }
        public void PlayUntilEnd()
        {
            for (int i=0;i >maxTurn;i++)
            {
                NextTurn();
                Render();
            }
        }
        public void NextTurn()
        {
            foreach(Unit unit in units)
            {
                unit.UpdateSensor(map);
                iOrder order = unit.GetOrder();
                order.Execute();
            }
            turn++;   
        }
        public void Render()
        {
            RenderBoard();
        }
        private void RenderBoard()
        {
            Console.Clear();
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
