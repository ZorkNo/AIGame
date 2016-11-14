using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class RandomAiType : IAiType
    {
        public Random Rnd;
        public IAi GetAi()
        {
            return new RandomAI(Rnd);
        }

        public void SetRandomGenerator(Random rnd)
        {
            Rnd=rnd;
        }
    }
    public class RandomAI:IAi
    {
        private Sensor Sensor;
        private Random Rnd;
        public RandomAI(Random rnd)
        {
            Rnd = rnd;
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
                     order = new Rotate(getDirection());
                    break;
                default:
                    throw new Exception("not a valid direction");
            }
            return order;
        }
        private RotateDirection getDirection()
        {

            int rndInt = Rnd.Next(1, 3);
            switch (rndInt)
            {
                case 1:
                    return RotateDirection.Left;
                case 2:
                    return RotateDirection.Right;

                default:
                     throw new Exception("not a valid direction");
            }
        }
    }

}
