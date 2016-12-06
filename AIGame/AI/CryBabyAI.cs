using System;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
   public class CryBabyAI : BaseAi
   {
        private int _turn = 0;
        public CryBabyAI(Random random, params string[] args) : base(random,args) { }

        public override IOrder GetOrder(Sensor sensor)
        {
            _turn++;
            if (_turn % 2 == 0)
            {
                return new BroadcastOrder(new Broadcast { Message = "SOS", Type = BroadcastType.Open });
            }
            else
            {
                return new DoNothing();
            }
            
        }

        
    }
}
