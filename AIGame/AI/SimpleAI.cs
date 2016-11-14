using AIGame.CoreGame;
using AIGame.CoreGame.Orders;

namespace AIGame.AI
{
    public class SimpleAiType : IAiType
    {
        public IAi GetAi()
        {
            return new SimpleAi();
        }
    }
    public class SimpleAi:IAi
    {
        private int _turn=0;
        public Sensor Sensor;
        public IOrder GetOrder(Sensor sensor)
        {
            IOrder order;
            if(_turn==0)
            { 
                order= new Turn(Direction.East);
            }
            else
            {
                order = new Move();
            }
            
            _turn++;
            return order;
        }
        
    }

}
