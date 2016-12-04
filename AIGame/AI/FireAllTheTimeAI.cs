using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class FireAllTheTimeAi : BaseAi
    {
        public FireAllTheTimeAi(Random random, string[] args) : base(random, args) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            return new FireTorpedo(getCoordinates()); ;
        }

        private Tuple<int, int> getCoordinates()
        {
            int rndX = _random.Next(1, 6);
            int rndY = _random.Next(1, 4);
            return new Tuple<int, int>(rndX - 2, rndY);
        }
    }
}
