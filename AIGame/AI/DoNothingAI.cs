using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class DoNothingAIType : IAiType
    {
        public IAi GetAi()
        {
            return new DoNothingAI();
        }

        public void SetRandomGenerator(Random rnd)
        {
        }

        public string Name
        {
            get { return "DoNothingAi"; }
        }
    }
    public class DoNothingAI : IAi
    {
        public IOrder GetOrder(Sensor sensor)
        {
            return new DoNothing();
        }
    }
}
