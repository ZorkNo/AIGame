using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
   public class DoNothingAI : BaseAi
    {
        public DoNothingAI(Random random, string[] args) : base(random, args) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            return new DoNothing();
        }

        
    }
}
