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
        public readonly Type BlueAiType;
        public readonly Type RedAiType;
        private int Turn;
        private int MaxTurn = 1000;
        private IMap Map;
        private GameMode gameMode;
        public GameResult GameResult {
            get
            {
                return CalculateResult();
            }
        }
        public Game(Type blue, Type red, GameMode gameMode, Random rnd)
        {
            BlueAiType = blue;
            RedAiType = red;
            
            this.gameMode = gameMode;
            List<IUnit> units = AddUnits(this.gameMode, rnd);
            Tuple<int, int> size = GetGameSize(this.gameMode);
            Map = new Map(size.Item1, size.Item2, rnd, units);


        }

        private List<IUnit> AddUnits(GameMode gameMode, Random rnd)
        {
            List<IUnit> units = new List<IUnit>();
            units.Add(new Unit("A", Side.Blue, AIFactory.CreateAi(BlueAiType, rnd)));
            units.Add(new Unit("X", Side.Red, AIFactory.CreateAi(RedAiType, rnd)));

            if (gameMode == GameMode.HiddenInfo2ShipLarge)
            {
                units.Add(new Unit("B", Side.Blue, AIFactory.CreateAi(BlueAiType, rnd)));
                units.Add(new Unit("Y", Side.Red, AIFactory.CreateAi(RedAiType, rnd)));
            }
            return units;
        }

        private Tuple<int, int> GetGameSize(GameMode gameMode)
        {
            if(gameMode == GameMode.HiddenInfo1ShipSmall)
                return new Tuple<int, int>(10,10);

            if (gameMode == GameMode.HiddenInfo1ShipLarge || gameMode == GameMode.HiddenInfo2ShipLarge)
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
                        unit.LastOrder = order;
                    }
                }
                Turn++;
            }
        }
        public void Render()
        {
            string message = "";
            Console.WriteLine(Map.RenderArea());
            Console.WriteLine("Turn:" + Turn);
            Console.WriteLine("Messages:");
            foreach (IUnit unit in Map.Units)
            {
                
                    if (unit.Render() != string.Empty)
                        message = string.Format("{0}{1}{2}", message, unit.Render(), System.Environment.NewLine);
                    if (unit.LastOrder !=null && unit.LastOrder.Render() != string.Empty)
                        message = string.Format("{0}{1}{2}", message, unit.LastOrder.Render(), System.Environment.NewLine);
                
            }
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