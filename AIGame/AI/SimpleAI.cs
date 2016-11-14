using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class SimpleAiType : iAiType
    {
        public iAI GetAi()
        {
            return new SimpleAI();
        }
    }
    public class SimpleAI:iAI
    {
        private int turn=0;
        public Sensor Sensor;
        public IOrder GetOrder(Sensor sensor)
        {
            IOrder order;
            if(turn==0)
            { 
                order= new Turn(Direction.east);
            }
            else
            {
                order = new Move();
            }
            
            turn++;
            return order;
        }
        
    }

}
