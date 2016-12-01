using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class RunAwayAiType : IAiType
    {
        public IAi GetAi(Random rnd)
        {
            if (rnd == null)
                throw new NullReferenceException("rnd is null: No random generator");
            return new RunAwayAi(rnd);
        }
        public string Name
        {
            get { return "RunAwayAi"; }
        }
    }
    public class RunAwayAi: IAi
    {
        private int _turn=0;
        private int health = 0;
        public Random Rnd;

        public RunAwayAi(Random rnd)
        {
            Rnd = rnd;
        }
        public IOrder GetOrder(Sensor sensor)
        {
            _turn++;

            bool hit = false;
            if (sensor.Health != health)
            {
                health = sensor.Health;
                hit = true;
            }

            if (Rnd.Next(1, 100) > 75 && hit == false)
                return new FireTorpedo(getCoordinates());

            if (sensor.Infront.Type == TerrainType.Land || sensor.Infront.Type == TerrainType.Edge)
                return Rotate();

            if (Rnd.Next(1, 100) > 65 && hit==false)
                return Rotate();

            return new Move();
  
        }

        private IOrder Rotate()
        {
            IOrder order;
            RotateDirection rotate = RotateDirection.Left;

            if (Rnd.Next(1, 100) > 85)
                rotate = RotateDirection.Right;

            order = new Rotate(rotate);
            return order;
        }
        private Tuple<int, int> getCoordinates()
        {
            int rndX = Rnd.Next(1, 6);
            int rndY = Rnd.Next(1, 4);
            return new Tuple<int, int>(rndX - 2, rndY);
        }
    }

}
