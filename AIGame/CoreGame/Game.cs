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
        public GameResult GameResult {
            get{ //No more units
                if (Map.Units.TrueForAll(u => u.IsDead && (u.Owner == Side.Blue || u.Owner == Side.Red)))
                    return GameResult.Tie;

                //Only one type of units
                if (Map.Units.TrueForAll(u => u.IsDead && u.Owner == Side.Blue))
                    return GameResult.RedWin;

                if (Map.Units.TrueForAll(u => u.IsDead && u.Owner == Side.Red))
                    return GameResult.BlueWin;

                //Max turns
                if (Turn >= MaxTurn)
                    return GameResult.Tie;

                return GameResult.GameNotEnded;
            }
        }

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
                if (GameResult != GameResult.GameNotEnded)
                    break;

            }
        }
     

        public void NextTurn()
        {
            if (GameResult != GameResult.GameNotEnded)
            { 
                foreach (IUnit unit in Map.Units)
                {
                    if(!unit.IsDead)
                    { 
                        unit.UpdateSensor(Map);
                        IOrder order = unit.GetOrder();
                        if (order.IsValid(unit, Map))
                        { 
                            order.Execute(unit, Map);
                        }
                    }
                }
                Turn++;
            }
        }
        public void Render()
        {
            Console.WriteLine(Map.RenderArea());
            Console.WriteLine("Turn:" + Turn);
            if(GameResult != GameResult.GameNotEnded)
                Console.WriteLine("GameResult:{0}" , GameResult);
        }
    }
}
