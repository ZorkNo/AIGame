using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
   public class DoNothingAI : BaseAi
    {
        public DoNothingAI(Random random) : base(random) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            return new DoNothing();
        }

        
    }
}
