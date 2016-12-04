using System;
using System.Linq;
using AIGame.CoreGame;
using AIGame.CoreGame.Orders;
using Rotate = AIGame.CoreGame.Orders.Rotate;

namespace AIGame.AI
{
    public class SimplePlusAiType : IAiType
    {
        public IAi GetAi(Random rnd)
        {
            if (rnd == null)
                throw new NullReferenceException("rnd is null: No random generator");
            return new SimplePlusAi(rnd);
        }
        public string Name
        {
            get { return "SimplePlusAi"; }
        }
    }
    public class SimplePlusAi : IAi
    {
        private int _turn=0;
        public Random Rnd;
        private int fireCounter = 0;
        private Tuple<int, int> target;

        public SimplePlusAi(Random rnd)
        {
            Rnd = rnd;
        }
        public IOrder GetOrder(Sensor sensor)
        {
            _turn++;

            if (sensor.ScannedArea.Targets.Any())
            {
                fireCounter = 3;
                target = sensor.ScannedArea.Targets.First().RelativeCoordinates;
            }

            if (fireCounter > 0)
            {
                fireCounter--;
                return new FireTorpedo(target);
            }

            //Hvis det en lige tur scan
            if (_turn % 2 == 0)
                return new SonorScan();

            if (sensor.Infront.Type == TerrainType.Land || sensor.Infront.Type == TerrainType.Edge)
                return Rotate();

            if (Rnd.Next(1, 100) > 65)
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
    }

}
