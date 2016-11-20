using System;
using System.Collections.Generic;
using AIGame.AI;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{
    public class Game
    {
        public int Turn;
        public int MaxTurn = 100;
        public IMap Map;
        public bool GameEnded = false;

        public Game(IAiType blue, IAiType red,int xSize,int ySize,Random rnd)
        {
            List<IUnit> units = new List<IUnit>();

            units.Add(new Unit("A", Side.Blue, blue.GetAi()));
            units.Add(new Unit("X", Side.Red, red.GetAi()));
            //units.Add(new Unit("B", Side.Blue, blue.GetAi()));
            //units.Add(new Unit("Y", Side.Red, red.GetAi()));
            Map = new Map(xSize, ySize, rnd, units);

            
        }
        public void PlayUntilEnd()
        {
            for (int i=0;i >MaxTurn;i++)
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
            foreach(IUnit unit in Map.Units)
            {
                unit.UpdateSensor(Map);
                IOrder order = unit.GetOrder();
                if (order.IsValid(unit, Map))
                { 
                    order.Execute(unit, Map);
                }
            }
            IsGameEnded();
            Turn++;   
        }
        public void Render()
        {
            RenderBoard();
        }
        private void RenderBoard()
        {
            Console.WriteLine(Map.RenderArea());
            Console.WriteLine("Turn:" + Turn);
        }
    }
}
