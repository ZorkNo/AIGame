using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using AIGame.Interfaces;

namespace AIGame.AI
{
    public abstract class BaseAi : IAi

    {
        protected Random _random;
        protected BaseAi(Random random, string[] args)
        {
            if (random == null)
                throw new ArgumentNullException(nameof(random));
            _random = random;
        }

        public abstract IOrder GetOrder(Sensor sensor);
    }
}
