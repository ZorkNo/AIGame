using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class RandomAiType : iAiType
    {
        public Random Rnd;
        public iAI GetAi()
        {
            return new RandomAI(Rnd);
        }
    }
    public class RandomAI:iAI
    {
        private Sensor Sensor;
        private Random Rnd;
        public RandomAI(Random rnd)
        {
            if (rnd==null)
            { 
                Rnd = new Random();
            }
            else
            {
                Rnd = rnd;
            }
        }
        public IOrder GetOrder(Sensor sensor)
        {
            IOrder order;

            order = getRandomOrder();

            return order;
        }

        private IOrder getRandomOrder()
        {
            IOrder order;  
            
            int rndInt = Rnd.Next(1,3);
            switch (rndInt)
            {
                case 1:
                    order = new Move();
                    break;
                case 2:
                     order = new Turn(getDirection());
                    break;
                default:
                    throw new Exception("not a valid direction");
            }
            return order;
        }
        private Direction getDirection()
        {
           
            int rndInt = Rnd.Next(1,5);
            switch (rndInt)
            {
                case 1:
                    return Direction.north;
                case 2:
                    return Direction.south;
                case 3:
                    return Direction.east;
                case 4:
                    return Direction.west;
                default:
                     throw new Exception("not a valid direction");
            }
        }
    }

}
