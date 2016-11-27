using System;
using System.Collections.Generic;
using System.Linq;
using AIGame.AI;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;

namespace AIGame.CoreGame
{
    public class Game
    {
        private int Turn;
        private int MaxTurn = 1000;
        private IMap Map;
        private GameMode gameMode;
        private string message="";
        public GameResult GameResult {
            get
            {
                return CalculateResult();
            }
        }
        public Game(IAiType blue, IAiType red, GameMode gameMode, Random rnd)
        {
            List<IUnit> units = new List<IUnit>();
            this.gameMode = gameMode;
            units.Add(new Unit("A", Side.Blue, blue.GetAi()));
            units.Add(new Unit("X", Side.Red, red.GetAi()));
            //units.Add(new Unit("B", Side.Blue, blue.GetAi()));
            //units.Add(new Unit("Y", Side.Red, red.GetAi()));
            Tuple<int, int> size = GetGameSize(this.gameMode);
            Map = new Map(size.Item1, size.Item2, rnd, units);


        }

        private Tuple<int, int> GetGameSize(GameMode gameMode)
        {
            if(gameMode == GameMode.HiddenInfo1ShipSmallNoBroadcast)
                return new Tuple<int, int>(10,10);

            if (gameMode == GameMode.HiddenInfo1ShipLargeNoBroadcast || gameMode == GameMode.HiddenInfo2ShipLargeNoBroadcast)
                return new Tuple<int, int>(20, 20);

            return new Tuple<int, int>(10, 10);
        }
        public void PlayUntilEnd()
        {
            while (true)
            {
                NextTurn();
                if (GameResult != GameResult.GameNotEnded)
                    break;

            }
        }
     

        public void NextTurn()
        {
            if (GameResult == GameResult.GameNotEnded)
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
                        if (unit.Render() != string.Empty)
                            message = string.Format("{0}{1}{2}", message, unit.Render(), System.Environment.NewLine);
                        if (order.Render()!=string.Empty)
                            message = string.Format("{0}{1}{2}", message, order.Render(), System.Environment.NewLine);
                    }
                }
                Turn++;
            }
        }
        public void Render()
        {
            Console.WriteLine(Map.RenderArea());
            Console.WriteLine("Turn:" + Turn);
            Console.WriteLine("Messages:");
            Console.WriteLine(message);

            if(GameResult != GameResult.GameNotEnded)
                Console.WriteLine("Result:{0}" , GameResult);

            message = string.Empty;
        }
        private GameResult CalculateResult()
        {
            //No more units
            if (Map.Units.FindAll(u => u.Owner == Side.Blue || u.Owner == Side.Red).TrueForAll(u => u.IsDead))
                return GameResult.Tie;

            //Only one type of units
            if (Map.Units.FindAll( u => u.Owner == Side.Blue).TrueForAll(u => u.IsDead))
                return GameResult.RedWin;

            if (Map.Units.FindAll(u => u.Owner == Side.Red).TrueForAll(u => u.IsDead))
                return GameResult.BlueWin;

            //Max turns
            if (Turn >= MaxTurn)
            {
                int blueHealth = Map.Units.Where(u => u.Owner == Side.Blue).Sum(u => u.Health);
                int redHealth = Map.Units.Where(u => u.Owner == Side.Red).Sum(u => u.Health);

                if (blueHealth == redHealth)
                    return GameResult.Tie;
                if (blueHealth > redHealth)
                    return GameResult.BlueWin;
                if (blueHealth < redHealth)
                    return GameResult.RedWin;
            }

            return GameResult.GameNotEnded;
        }
    }
}
