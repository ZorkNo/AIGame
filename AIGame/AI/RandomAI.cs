using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class RandomAI : BaseAi
    {
        public RandomAI(Random random, string[] args) : base(random, args) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            IOrder order;

            order = getRandomOrder();

            return order;
        }

        private IOrder getRandomOrder()
        {
            IOrder order;  
            
            int rndInt = _random.Next(1,4);
            switch (rndInt)
            {
                case 1:
                    order = new Move();
                    break;
                case 2:
                     order = new Rotate(getDirection());
                    break;
                case 3:
                    order = new FireTorpedo(getCoordinates());
                    break;
                default:
                    throw new Exception("not a valid direction");
            }
            return order;
        }

        private Tuple<int, int> getCoordinates()
        {
            int rndX = _random.Next(1, 6);
            int rndY = _random.Next(1, 4);
            return new Tuple<int, int>(rndX-2, rndY);
        }

        private RotateDirection getDirection()
        {

            int rndInt = _random.Next(1, 3);
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
